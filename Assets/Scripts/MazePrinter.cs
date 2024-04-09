using System.Collections.Generic;
using Mazes.Contracts;
using UnityEngine;

public class MazePrinter<T>: IMazePrinter<T> where T: Object
{
    private readonly T wallPrefab;
    private readonly GameObject parent;
    private int drawColumn;
    private Vector3 startPosition;

    private IList<T> allBricks;

    public MazePrinter(T wallPrefab, GameObject parent)
    {
        this.allBricks = new List<T>();
        this.wallPrefab = wallPrefab;
        this.parent = parent;
    }
    
    public void DrawMaze(Maze<T> maze, MazeVector startMazeVector)
    {
        this.DrawMaze(maze.Cells, startMazeVector, maze.Title);
    }

    public void DrawMaze(Cell<T>[,] cells, MazeVector startMazeVector, string title, bool drawItems = false)
    {
        foreach (var brick in this.allBricks)
        {
            Object.Destroy(brick);    
        }
        
        this.startPosition = new Vector3(startMazeVector.X, startMazeVector.Y, startMazeVector.Z);
        var width = cells.GetLength(0);
        var height = cells.GetLength(1);
        
        this.DrawHorizontalWall(cells, 0.5f);
        this.DrawHorizontalWall(cells, 100f);
        this.DrawVerticalWall(cells, 93.5f);
        this.DrawVerticalWall(cells, -9f);
        
        for (int row = 0; row < height; row++)
        {
             for (int column = 0; column < width; column++)
             {
                 DrawVerticalSegment(cells, column, row, 1.25f);
                 DrawHorizontalSegment(cells, column, row, -10f);
             }
        }
    }
    
    private void DrawVerticalSegment(Cell<T>[,] cells, int column, int row, float offsetX)
    {
        float width = 10.25f;
        float offsetZ = 9.96f;

        if (column < cells.GetLength(0) - 1)
        {
            if (!cells[column, row].LinkedCells.Contains(cells[column, row].EasternNeighbour))
            {
                var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                    this.startPosition.x + offsetX + width * column,
                    this.startPosition.y,
                    this.startPosition.z - offsetZ * row), Quaternion.identity, parent.transform);
            
                this.allBricks.Add(segment);
            }
        }
    }
    
    private void DrawHorizontalSegment(Cell<T>[,] cells, int column, int row, float offsetZ)
    {
        float height = 10f;
        float offsetX = 10f;

        if (row < cells.GetLength(1) - 1)
        {
            if (!cells[column, row].LinkedCells.Contains(cells[column, row].SouthernNeighbour))
            {
                var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                    this.startPosition.x + offsetX * column,
                    this.startPosition.y,
                    this.startPosition.z + offsetZ - height * row), Quaternion.Euler(0,90,0), parent.transform);  
            
                this.allBricks.Add(segment);
            }   
        }
    }
    
    private void DrawHorizontalWall(Cell<T>[,] cells, float offsetZ)
    {
        float offset = 10f;
        var width = cells.GetLength(0);
        
        for (int column = 0; column < width; column++)
        {
            var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + offset * column,
                this.startPosition.y,
                this.startPosition.z - offsetZ), Quaternion.Euler(0,90,0), parent.transform);
            
            this.allBricks.Add(segment);
        }
    }
    
    private void DrawVerticalWall(Cell<T>[,] cells, float offsetX)
    {
        float offsetZ = 10f;
        
        var width = cells.GetLength(0);
        
        for (int column = 0; column < width; column++)
        {
            var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + offsetX,
                this.startPosition.y,
                this.startPosition.z - offsetZ * column), Quaternion.identity, parent.transform);
            
            this.allBricks.Add(segment);
        }
    }
    
    public void DrawCellItems(Maze<T> maze)
    {
        DrawCellItems(maze.Cells);
    }

    public void DrawCellItems(Cell<T>[,] cells)
    {
        
    }

    public void DrawItemAtPosition(Cell<T>[,] cells, MazeVector position, T item)
    {
        float transX = -5f;
        float transZ = -4f;
        float offsetX = 10.25f;
        float offsetZ = 10f;
        
        Object.Instantiate(item, new Vector3(
            this.startPosition.x + transX + offsetX * position.X,
            this.startPosition.y,
            this.startPosition.z + transZ - offsetZ * position.Y), Quaternion.identity);
    }

    public IList<T> Items { get; set; }
}
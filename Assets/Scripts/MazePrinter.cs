using System.Collections.Generic;
using Mazes.Contracts;
using UnityEngine;

public class MazePrinter<T>: IMazePrinter<T> where T: Object
{
    private readonly T wallPrefab;
    private int drawColumn;
    private Vector3 startPosition;

    private IList<T> allBricks;

    public MazePrinter(T wallPrefab)
    {
        this.allBricks = new List<T>();
        this.wallPrefab = wallPrefab;
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
        
        this.DrawNorthWall(cells);
        
        for (int row = 0; row < height; row++)
        {
            DrawWestWallSegment(row);
        
            for (int column = 0; column < width; column++)
            {
                DrawEastWallSegment(cells, column, row);
                DrawSouthWallSegment(cells, column, row);
            }
        }
    }

    private void DrawWestWallSegment(int row)
    {
        float offsetZ = 10f;
        float offsetX = 99f;
        
        var segment = Object.Instantiate(this.wallPrefab, new Vector3(
            this.startPosition.x - offsetX,
            this.startPosition.y,
            this.startPosition.z - offsetZ * row), Quaternion.identity);
        
        this.allBricks.Add(segment);
    }
    
    private void DrawEastWallSegment(Cell<T>[,] cells, int column, int row)
    {
        float offsetX = 10f;
        float offsetZ = 10f;
        
        if (!cells[column, row].LinkedCells.Contains(cells[column, row].EasternNeighbour))
        {
            var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + 3.76f - offsetX * column,
                this.startPosition.y,
                this.startPosition.z - offsetZ * row), Quaternion.identity);

            // if (!cells[column, row].LinkedCells.Contains(cells[column, row].SouthernNeighbour) ||
            //     cells[column, row].EasternNeighbour != null && 
            //     !cells[column, row].EasternNeighbour.LinkedCells.Contains(cells[column, row].EasternNeighbour.SouthernNeighbour))
            // {
            //     (segment as GameObject)!.transform.localScale = new Vector3(1, 1, 0.75f);   
            // }
            
            this.allBricks.Add(segment);
        }
    }
    
    private void DrawSouthWallSegment(Cell<T>[,] cells, int column, int row)
    {
        float offset = 10;

        if (!cells[column, row].LinkedCells.Contains(cells[column, row].SouthernNeighbour))
        {
            var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x - offset * column,
                this.startPosition.y,
                this.startPosition.z - offset * (row + 1)), Quaternion.Euler(0,90,0));  
            
            this.allBricks.Add(segment);
        }
    }

    private void DrawNorthWall(Cell<T>[,] cells)
    {
        float offset = 10f;
        float offsetZ = .5f;
        var width = cells.GetLength(0);
        
        for (int column = 0; column < width; column++)
        {
            var segment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x - offset * column,
                this.startPosition.y,
                this.startPosition.z - offsetZ), Quaternion.Euler(0,90,0));
            
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
        float offsetX = 10f;
        float offsetZ = 10f;
        
        Object.Instantiate(item, new Vector3(
            this.startPosition.x - offsetX * position.Y,
            this.startPosition.y,
            this.startPosition.z - 0.40f - offsetZ * (position.X + 1)), Quaternion.LookRotation(Camera.main.transform.position));
    }

    public IList<T> Items { get; set; }
}
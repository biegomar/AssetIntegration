using System.Collections.Generic;
using Mazes.Contracts;
using UnityEngine;

public class MazePrinter<T>: IMazePrinter<T> where T: Object
{
    private readonly T wallPrefab;
    private readonly T polePrefab;
    private readonly GameObject parent;
    private int drawColumn;
    private Vector3 startPosition;

    private IList<T> allBricks;

    public MazePrinter(T wallPrefab, T polePrefab, GameObject parent)
    {
        this.allBricks = new List<T>();
        this.wallPrefab = wallPrefab;
        this.polePrefab = polePrefab;
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
        
        for (int row = 0; row < height; row++)
        {
             for (int column = 0; column < width; column++)
             {
                 DrawCell(cells[column, row], column, row);
             }
        }
    }

    private void DrawCell(Cell<T> cell, int column, int row)
    {
        var tileLength = 10f;
        var pivotOffset = 0.5f;
        
        var topPole = Object.Instantiate(this.polePrefab, new Vector3(
            this.startPosition.x + tileLength * column + column + pivotOffset,
            this.startPosition.y,
            this.startPosition.z - tileLength * row - row), Quaternion.identity, parent.transform);

        topPole.name = $"TopPole_{column}_{row}";
        
        this.allBricks.Add(topPole);

        // I am a eastern wall cell
        if (cell.EasternNeighbour == default)
        {
            var topEasternPole = Object.Instantiate(this.polePrefab, new Vector3(
                this.startPosition.x + tileLength * (column + 1) + column + pivotOffset,
                this.startPosition.y,
                this.startPosition.z - tileLength * row - row), Quaternion.identity, parent.transform);
            
            topEasternPole.name = $"TopPole_{column}_{row}";
            
            this.allBricks.Add(topEasternPole);
            
            var eastWallSegment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + tileLength * (column + 1) + column + pivotOffset,
                this.startPosition.y,
                this.startPosition.z - tileLength * row - row -  pivotOffset), Quaternion.Euler(0,90,0), parent.transform); 
            
            eastWallSegment.name = $"EastWallSegment{column}_{row}";
            
            this.allBricks.Add(eastWallSegment);
        }
        
        // I am a southern wall cell
        if (cell.SouthernNeighbour == default)
        {
            var southernPole = Object.Instantiate(this.polePrefab, new Vector3(
                this.startPosition.x + tileLength * column + column + pivotOffset,
                this.startPosition.y,
                this.startPosition.z - tileLength * (row + 1) - row), Quaternion.identity, parent.transform);
            
            southernPole.name = $"SouthernPole{column}_{row}";
            
            this.allBricks.Add(southernPole);
            
            var southernWallSegment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + tileLength * column + column + 1,
                this.startPosition.y,
                this.startPosition.z - tileLength * (row + 1) - row), Quaternion.identity, parent.transform);
            
            southernWallSegment.name = $"SouthernWallSegment{column}_{row}";
            
            this.allBricks.Add(southernWallSegment);
        }
        
        // I am a western wall cell or I have a western linked neighbour.
        if (cell.WesternNeighbour == default || !cell.LinkedCells.Contains(cell.WesternNeighbour))
        {
            var westWallSegment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + tileLength * column + column + pivotOffset,
                this.startPosition.y,
                this.startPosition.z - tileLength * row - row - pivotOffset), Quaternion.Euler(0,90,0), parent.transform);
            
            westWallSegment.name = $"WestWallSegment{column}_{row}";
            
            this.allBricks.Add(westWallSegment);
        }
        
        // I am a northern wall cell or I have a northern linked neighbour.
        if (cell.NothernNeighbour == default || !cell.LinkedCells.Contains(cell.NothernNeighbour))
        {
            var northWallSegment = Object.Instantiate(this.wallPrefab, new Vector3(
                this.startPosition.x + tileLength * column + column + 1,
                this.startPosition.y,
                this.startPosition.z - tileLength * row - row), Quaternion.identity, parent.transform);
            
            northWallSegment.name = $"NorthWallSegment{column}_{row}";
            
            this.allBricks.Add(northWallSegment);
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
        var tileLength = 10f;
        var xOffset = 5f;
        var yOffset = 5f;

        var newPosition = new Vector3(
            this.startPosition.x + tileLength * position.X + position.X + xOffset,
            this.startPosition.y,
            this.startPosition.z - tileLength * position.Y - position.Y - yOffset);
        
         var enemy = Object.Instantiate(item, newPosition, Quaternion.identity, parent.transform);
    }

    public IList<T> Items { get; set; }
}
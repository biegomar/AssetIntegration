using System;
using System.Collections;
using System.Collections.Generic;
using AldousBroderMaze;
using UnityEngine;
using BinaryTreeMaze;
using DefaultNamespace;
using Mazes.Contracts;
using SideWinderMaze;
using Unity.AI.Navigation;

public class MazeController : MonoBehaviour
{
    
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject pole;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2Int dimension;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private NavMeshSurface mesh;
    [SerializeField] private MazeStrategies mazeStrategie;
    
    private Maze<GameObject> maze;

    private void Awake()
    {
        var mazeDimension = new MazeVector(dimension.x, dimension.y, 0);
        
        switch (mazeStrategie)
        {
            case MazeStrategies.EmptyMaze:
                this.maze = new Maze<GameObject>(mazeDimension, new EmptyMazeGenerator<GameObject>(),
                    new MazePrinter<GameObject>(this.wall, this.pole, this.gameObject), "Empty");
                break;
            case MazeStrategies.FullMaze:
                this.maze = new Maze<GameObject>(mazeDimension, new FullMazeGenerator<GameObject>(),
                    new MazePrinter<GameObject>(wall, pole, this.gameObject), "Full");
                break;
            case MazeStrategies.AldousBroder:
                this.maze = new Maze<GameObject>(mazeDimension,
                    new AldousBroderMazeGenerator<GameObject>(null),
                    new MazePrinter<GameObject>(wall, pole, this.gameObject), "Aldous Broder");
                break;
            case MazeStrategies.BinaryTree:
                this.maze = new Maze<GameObject>(mazeDimension, new BinaryTreeMazeGenerator<GameObject>(),
                    new MazePrinter<GameObject>(this.wall, this.pole, this.gameObject), "Binary Tree");
                break;
            case MazeStrategies.SideWinder:
                this.maze = new Maze<GameObject>(mazeDimension, new SideWinderMazeGenerator<GameObject>(),
                    new MazePrinter<GameObject>(this.wall, this.pole, this.gameObject), "Sidewinder");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void Start()
    {
        this.maze.Draw(new MazeVector(Convert.ToInt32(startPoint.x), Convert.ToInt32(startPoint.y),Convert.ToInt32(startPoint.z)));
        
        this.maze.DrawItemAtPosition(new MazeVector(0,0,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(2,2,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(4,4,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(6,6,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(8,8,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(10,10,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(12,12,0), enemy);
        //this.maze.DrawItemAtPosition(new MazeVector(14,14,0), enemy);
        //this.maze.DrawItemAtPosition(new MazeVector(16,16,0), enemy);
        //this.maze.DrawItemAtPosition(new MazeVector(18,18,0), enemy);
        //this.maze.DrawItemAtPosition(new MazeVector(19,19,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(5,5,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(9,1,0), enemy);
        
        mesh.BuildNavMesh();
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using AldousBroderMaze;
using UnityEngine;
using BinaryTreeMaze;
using Mazes.Contracts;
using SideWinderMaze;

public class MazeController : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject enemy;
    
    private Maze<GameObject> maze;

    private void Awake()
    {
        var dimension = new MazeVector(10, 10, 0);
        //this.maze = new Maze(dimension, new BinaryTreeMazeGenerator(), new MazePrinter(wall), "Binary Tree");
        //this.maze = new Maze<GameObject>(dimension, new SideWinderMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall), "Sidewinder");
        this.maze = new Maze<GameObject>(dimension, new FullMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall), "Full");
        //this.maze = new Maze<GameObject>(dimension, new EmptyMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall), "Empty");

        // this.maze = new Maze<GameObject>(dimension,
        //     new AldousBroderMazeGenerator<GameObject>(null, 1),
        //     new MazePrinter<GameObject>(wall), "Aldous Broder");
    }

    void Start()
    {
        this.maze.Draw(new MazeVector(65, 0, 65));
        
        this.maze.DrawItemAtPosition(new MazeVector(0,0,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(5,5,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(9,1,0), enemy);
        
    }
}

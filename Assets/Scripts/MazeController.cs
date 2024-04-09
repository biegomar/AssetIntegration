using System;
using System.Collections;
using System.Collections.Generic;
using AldousBroderMaze;
using UnityEngine;
using BinaryTreeMaze;
using Mazes.Contracts;
using SideWinderMaze;
using Unity.AI.Navigation;

public class MazeController : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private NavMeshSurface mesh;
    
    private Maze<GameObject> maze;

    private void Awake()
    {
        var dimension = new MazeVector(10, 10, 0);
        this.maze = new Maze<GameObject>(dimension, new BinaryTreeMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall, this.gameObject), "Binary Tree");
        //this.maze = new Maze<GameObject>(dimension, new SideWinderMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall, this.gameObject), "Sidewinder");
        //this.maze = new Maze<GameObject>(dimension, new FullMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall, this.gameObject), "Full");
        //this.maze = new Maze<GameObject>(dimension, new EmptyMazeGenerator<GameObject>(), new MazePrinter<GameObject>(wall, this.gameObject), "Empty");

        // this.maze = new Maze<GameObject>(dimension,
        //     new AldousBroderMazeGenerator<GameObject>(null),
        //     new MazePrinter<GameObject>(wall, this.gameObject), "Aldous Broder");
    }

    void Start()
    {
        this.maze.Draw(new MazeVector(Convert.ToInt32(startPoint.x), Convert.ToInt32(startPoint.y),Convert.ToInt32(startPoint.z)));
        
        this.maze.DrawItemAtPosition(new MazeVector(0,0,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(5,5,0), enemy);
        this.maze.DrawItemAtPosition(new MazeVector(9,1,0), enemy);
        
        mesh.BuildNavMesh();
        
    }
}

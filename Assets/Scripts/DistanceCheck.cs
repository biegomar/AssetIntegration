using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DistanceCheck : MonoBehaviour
{
    public class PositionData
    {
        public Vector3 Position;
    }

    [SerializeField] private int numberOfObjects;
    [SerializeField] private Vector3 dimensions;
    
    public List<PositionData> PositionDatas = new List<PositionData>();
    
    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            this.PositionDatas.Add(new PositionData() {Position = new Vector3(Random.Range(0, dimensions.x), Random.Range(0, dimensions.x), Random.Range(0, dimensions.x))});
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var positionData in PositionDatas)
        {
            Gizmos.DrawSphere(positionData.Position, 0.1f);
        }
    }
}

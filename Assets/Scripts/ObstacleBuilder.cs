using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{
    public enum ObstacleState
    {
        Open, Blocked, 
    }

    [Serializable]
    public struct Obstacle {
        GameObject prefab;

    }

    [SerializeField]
    private Obstacle[] obstacles;
}
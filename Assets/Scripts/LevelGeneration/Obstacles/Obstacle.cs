using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Obstacle 
{
    public GameObject prefab;
    public ObstacleState state;
    public int sinceChange;
    public float length;
}

public enum ObstacleState
{
    Open, Blocked, Half
}

public class ObstacleImplements : MonoBehaviour {
    
}
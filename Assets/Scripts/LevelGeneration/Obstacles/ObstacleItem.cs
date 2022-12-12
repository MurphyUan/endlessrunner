using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObstacleState
{
    Open, Full, Half
}

[Serializable]
public class ObstacleItem : MonoBehaviour {
    public GameObject Prefab;
    public ObstacleState State;
    public int Length;
}
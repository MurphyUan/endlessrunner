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
    [SerializeField] public GameObject Prefab;
    [SerializeField] public ObstacleState State;
    [SerializeField] public int Amount = 5;
}
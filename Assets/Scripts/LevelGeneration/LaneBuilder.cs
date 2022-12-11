using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuilder : MonoBehaviour
{
    [SerializeField] private int NumberOfPlatforms = 15;
    [SerializeField] private int NumberOfLanes = 3;
    [SerializeField] private float LaneWidth = 2;
    public static List<GameObject> LanePrefabs;

    public static GameObject Phantom;
    public static LaneBuilder Singleton;

    // State of Obstacles Stored here: 0 for Open/Half, 1 for Full
    private ObstacleState[] laneMatrix;
    private int maxMatrixLimit = 0;

    #region StartUp Methods

    private void Awake()
    {
        Phantom = new GameObject("phantom");

        laneMatrix = new ObstacleState[NumberOfLanes];
        for(int i = 0; i < NumberOfLanes; i++)
            maxMatrixLimit += (int)Mathf.Pow(2,i);
    }

    #endregion

    public static void RunPhantom()
    {

    }

    private static ObstacleState[] changeMatrix(){
        return null;
    }
}

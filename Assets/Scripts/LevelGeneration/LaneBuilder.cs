using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuilder : MonoBehaviour
{
    [SerializeField] int NumberOfPlatforms = 10;
    public static GameObject Phantom;
    public static bool PassedTurn;

    private void Awake(){
        Phantom = new GameObject("phantom");
    }

    public static void BuildLane()
    {
        Phantom = new GameObject();
        ConstructLane();
    }

    private static void ConstructLane(){
        
    }
}

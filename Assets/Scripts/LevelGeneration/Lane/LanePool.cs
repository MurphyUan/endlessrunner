using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePool : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    public List<GameObject> AvailableLanes;

    public static LanePool Singleton;
    
    private void Awake() 
    {
        Singleton = this;    
    }
}

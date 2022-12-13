using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public static GameObject Player;
    public static GameObject CurrentPlatform;

    private void Awake() 
    {
        Player = this.gameObject;    
    }

    private void Start(){
        foreach(int i in Enumerable.Range(0, LaneBuilder.Singleton.NumberOfPlatforms))
            LaneBuilder.RunPhantom();
    }

    private void OnCollisionEnter(Collision other) {
        if(CurrentPlatform != other.gameObject){
            LaneBuilder.RunPhantom();
            CurrentPlatform = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.GetType().ToString()){
            case "BoxCollider":{

                break;
            }
            case "CapsuleCollider":{
                // Collect Powerup - Coin
                break;
            }
            default:break;
        }
    }
}

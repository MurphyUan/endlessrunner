using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public static GameObject Player;
    public static GameObject CurrentPlatform;

    public static bool CanTurn = false;

    private void Awake() 
    {
        Player = this.gameObject;    
    }

    private void OnCollisionEnter(Collision other) {
        CurrentPlatform = other.gameObject;
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.GetType().ToString()){
            case "SphereCollider":{
                CanTurn = true;
                break;
            }
            case "BoxCollider":{
                LaneBuilder.BuildLane();
                break;
            }
            case "CapsuleCollider":{
                // Collect Powerup - Coin
                break;
            }
            default:break;
        }
    }

    private void OnTriggerExit(Collider other) {
        switch(other.GetType().ToString()){
            case "SphereCollider":{
                CanTurn = false;
                break;
            }
            default:break;
        }    
    }

    
}

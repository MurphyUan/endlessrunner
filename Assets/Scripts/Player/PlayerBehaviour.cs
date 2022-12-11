using System;
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

    private void OnCollisionEnter(Collision other) {
        CurrentPlatform = other.gameObject;
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.GetType().ToString()){
            case "BoxCollider":{
                LaneBuilder.RunPhantom();
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

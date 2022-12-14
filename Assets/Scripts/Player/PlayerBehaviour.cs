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

    private void OnCollisionEnter(Collision other) {
        if(CurrentPlatform != other.gameObject){
            if(LevelHandler.spawnPlatforms)
                LaneBuilder.RunPhantom();
            CurrentPlatform = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.GetType().ToString()){
            case "BoxCollider":{
                Utils.PlayerDeathEvent();
                break;
            }
            case "SphereCollider":{
                Utils.PlayerCoinEvent(other.GetComponent<Coin>());
                break;
            }
            default:break;
        }
    }
}

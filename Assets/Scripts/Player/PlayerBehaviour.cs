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

    private void FixedUpdate() {
        if(Player.transform.position.y < -1)
            Utils.PublishPlayerDeathEvent();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle"){
            Utils.PublishPlayerDeathEvent();
            return;
        }
        if(CurrentPlatform != other.gameObject){
            if(LevelHandler.spawnPlatforms)
                LaneBuilder.RunPhantom();
            CurrentPlatform = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.tag){
            case "Obstacle":{
                Debug.Log("Player Dies");
                Utils.PublishPlayerDeathEvent();
                break;
            }
            case "Coin": {
                Utils.PublishPlayerCoinEvent(other.GetComponent<Coin>());
                break;
            }
            case "Ground": {
                PlayerMovement.endJump();
                break;
            }
            default:{
                break;
            };
        }
    }
}

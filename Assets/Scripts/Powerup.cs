using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string PowerupName;
    
    private void OnEnable() {
        Utils.PlayerPowerupEvent += PowerupPickUpEvent;
    }

    private void OnDisable() {
        
    }

    private void PowerupPickUpEvent(string powerupName){
        switch(powerupName){
            case "Magnet":{
                break;
            }
        }
    }

    private void OnMagnetPickup(){

    }

}

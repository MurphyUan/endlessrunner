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
        Utils.PlayerPowerupEvent -= PowerupPickUpEvent;
    }

    private void PowerupPickUpEvent(string powerupName){
        switch(powerupName){
            case "Magnet":{
                Coin.isMagnetised = true;
                PlayerMovement.DelayedCall(10, EndMagnetism);
                break;
            }
        }
    }

    private void EndMagnetism(){
        Coin.isMagnetised = false;
    }
}

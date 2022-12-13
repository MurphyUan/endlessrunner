using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int ScoreValue = 10;
    public static bool isMagnetised = false;

    private void FixedUpdate() {
        if(isMagnetised){
            // Get Player Location
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            this.gameObject.SetActive(false);
    }
}

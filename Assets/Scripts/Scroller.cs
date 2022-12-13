using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private static float moveSpeed = 5f;
    private float defaultSpeed = 5f;

    private void FixedUpdate() 
    {
        this.transform.position -= (PlayerBehaviour.Player.transform.forward * moveSpeed) * Time.fixedDeltaTime;
    }

    private static IEnumerator StartScroller(){
        int time = 0;
        while(true)
        {
            time++;
            
            yield return new WaitForSeconds(1);
        }
    } 
}

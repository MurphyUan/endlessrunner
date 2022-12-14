using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private static float moveSpeed = 5f;

    public static float timeElapsed = 1;

    private void FixedUpdate() 
    {
        this.transform.position -= (PlayerBehaviour.Player.transform.forward * moveSpeed) * Time.fixedDeltaTime;
    }

    public static IEnumerator startScroller()
    {
        while(true)
        {
            if(Time.timeScale == 0)break;
            float localSpeed = Mathf.Sqrt(timeElapsed) * 2;
            if(moveSpeed < localSpeed) moveSpeed = localSpeed;
            
            yield return new WaitForSeconds(1);
            timeElapsed += moveSpeed * Time.fixedDeltaTime;
        }
    } 
}

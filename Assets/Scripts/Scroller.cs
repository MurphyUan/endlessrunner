using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private static float moveSpeed = 5f;

    private static float timeElapsed = 1;

    private static Coroutine scrolling;

    private void FixedUpdate() 
    {
        this.transform.position -= (PlayerBehaviour.Player.transform.forward * moveSpeed) * Time.fixedDeltaTime;
    }

    private static IEnumerator startScroller()
    {
        while(true)
        {
            float localSpeed = Mathf.Sqrt(timeElapsed);
            if(moveSpeed < localSpeed) moveSpeed = localSpeed;
            
            yield return new WaitForSeconds(1);
            timeElapsed++;
        }
    } 

    public void StartScroller()
    {
        scrolling = StartCoroutine("startScroller");
    }

    public void stopScroller()
    {
        if(scrolling == null){
            Debug.Log("Scroller isn't scrolling");
            return;
        }
        StopCoroutine(scrolling);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public static float moveSpeed = 0.1f;

    public static float timeElapsed = 1;

    private void FixedUpdate() 
    {
        this.transform.position -= (PlayerBehaviour.Player.transform.forward * moveSpeed);
    }

    public static IEnumerator startScroller()
    {
        while(true)
        {
            if(Time.timeScale == 0)break;
            if(moveSpeed < LevelHandler.Singleton.MaxMoveSpeed)
                moveSpeed += Time.fixedDeltaTime * LevelHandler.Singleton.SpeedIncrease;
            yield return new WaitForSeconds(1);
        }
    } 
}

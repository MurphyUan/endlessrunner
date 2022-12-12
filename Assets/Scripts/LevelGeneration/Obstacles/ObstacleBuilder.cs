using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRowBuilder : MonoBehaviour
{

    public static ObstacleRowBuilder Singleton;

    #region StartUp Methods

    private void Awake()
    {
        Singleton = this;
    }

    #endregion

    public static List<ObstacleWithInstance> BuildRow(List<ObstacleItem> currentRow, int length)
    {
        return buildRow(currentRow, length);
    }

    private static List<ObstacleWithInstance> buildRow(List<ObstacleItem> currentRow, int length)
    {
        if(currentRow == null) return returnOpenRow(length);

        List<ObstacleItem> futureRow = new List<ObstacleItem>(currentRow);

        int stateIndex = 0;
        bool hasHalf = false;

        foreach(ObstacleItem item in futureRow){
            int index = item.State == ObstacleState.Full ? 1 : 0;
            stateIndex += (int)Math.Pow(2, index);
            if(item.State == ObstacleState.Half) hasHalf = true;
        }

        switch(stateIndex){
            case 0:{
                break;
            }
            case 1:{
                break;
            }
            case 3:{
                break;
            }
            default:{
                break;
            }
        }

        

        return returnOpenRow(length);
    }

    private static List<ObstacleItem> GetNewStates(List<ObstacleItem> items)
    {
        return null;
    }

    private static bool TrySwap(bool state)
    {

        return state;
    }

    private static bool CheckSwap(ObstacleItem obstacle)
    {
        int random = UnityEngine.Random.Range(0, 6);
        return random > 1;
    }

    private static List<ObstacleWithInstance> returnOpenRow(int length){
        List<ObstacleWithInstance> localArray = new List<ObstacleWithInstance>();
        for(int i = 0; i < length; i++){
            localArray.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Open));
        }
        return localArray;
    }

    #region Old Code

    // private (bool, ObstacleState) returnUpdatedState(Obstacle obstacle)
    // {
    //     bool shouldSwapState = CheckSwap(obstacle);
    //     ObstacleState swappedState = obstacle.state;

    //     switch(obstacle.state){
    //         case ObstacleState.Open:{
    //             if(shouldSwapState) {
    //                 // Choose between Half or Closed
    //                 if(UnityEngine.Random.Range(0,2) == 0)
    //                     swappedState = ObstacleState.Half;
    //                 else 
    //                     swappedState = ObstacleState.Blocked;
    //                 return (true, swappedState);
    //             } else
    //                 break;
    //         }
    //         case ObstacleState.Half:{
    //             swappedState = ObstacleState.Open;
    //             return (true, swappedState);
    //         }
    //         case ObstacleState.Full:{
    //             if(shouldSwapState) {
    //                 swappedState = ObstacleState.Open;
    //                 return (true, swappedState);
    //             } else
    //                 break;
    //         }
    //     }
    //     obstacle.sinceChange++;
    //     return (false, swappedState);
    // }
    #endregion
}
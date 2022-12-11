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

    public static List<ObstacleItem> BuildRow(List<ObstacleItem> currentState)
    {
        return buildRow(currentState);
    }

    private static List<ObstacleItem> buildRow(List<ObstacleItem> currentState)
    {
        if(currentState == null) return returnOpenRow(currentState.Count);

        List<ObstacleItem> futureRow = new List<ObstacleItem>();

        

        return returnOpenRow(currentState.Count);
    }

    private static bool CheckSwap(ObstacleItem obstacle)
    {
        int random = UnityEngine.Random.Range(0, 5);
        if(obstacle.Length < obstacle.NumChange) return false;
        return obstacle.NumChange > random;
    }

    private static List<ObstacleItem> returnOpenRow(int length){
        List<ObstacleItem> localArray = new List<ObstacleItem>();
        for(int i = 0; i < length; i++){
            localArray.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Open).Item);
        }
        return localArray;
    }

    #region Old Code
    // public static List<Obstacle> BuildObstacleRow(List<Obstacle> obstacles)
    // {
    //     foreach(Obstacle obstacle in obstacles)
    //     {
    //         var result = returnUpdatedState(obstacle);
    //         if (result.Item1) 
    //         {
    //             SwapObstacle(obstacle, result.Item2);
    //         }
    //     }
    //     return obstacles;
    // }

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

    // private bool CheckSwap(Obstacle obstacle)
    // {
    //     int random = UnityEngine.Random.Range(0, 5);
    //     if(obstacle.length < obstacle.sinceChange) return false;
    //     return obstacle.sinceChange > random;
    // }

    // private void SwapObstacle(Obstacle obstacle, ObstacleState state) 
    // {
    //     obstacle.sinceChange = 0;
    //     List<Obstacle> obstaclesOfState = statedObstacles[obstacle.state];
    //     int index = UnityEngine.Random.Range(0, obstaclesOfState.Count);
    //     obstacle = obstaclesOfState[index];
    // }
    #endregion
}
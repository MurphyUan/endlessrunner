using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{
    ObstaclePool obstacleDictionary;

    private static ObstacleBuilder instance = new ObstacleBuilder();
    private ObstacleBuilder(){}
    static ObstacleBuilder(){}

    public static ObstacleBuilder Instance
    {
        get { return instance;}
    }

    private void Awake() {
        obstacleDictionary = ObstaclePool.Singleton;
    }

    public static ObstacleItem UpdateObstacle(ObstacleItem obstacle)
    {
        if(obstacle.Length > obstacle.NumChange) return obstacle;
        return new ObstacleItem();
    }

    private static (bool, ObstacleState) checkSwapState(ObstacleItem obstacle)
    {
        return (false, ObstacleState.Open);
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
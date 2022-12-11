using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;

    private Dictionary<ObstacleState, List<Obstacle>> statedObstacles;

    void Awake()
    {
        statedObstacles = new Dictionary<ObstacleState, List<Obstacle>>();
        foreach (ObstacleState state in Enum.GetValues(typeof(ObstacleState))){
            statedObstacles.Add(state, new List<Obstacle>());
        }
        foreach(Obstacle obs in obstacles){
            statedObstacles[obs.state].Add(obs);
        }
    }

    public List<Obstacle> BuildObstacleRow(List<Obstacle> obstacles)
    {
        foreach(Obstacle obstacle in obstacles)
        {
            var result = returnUpdatedState(obstacle);
            if (result.Item1) 
            {
                SwapObstacle(obstacle, result.Item2);
            }
        }
        return obstacles;
    }

    private (bool, ObstacleState) returnUpdatedState(Obstacle obstacle)
    {
        bool shouldSwapState = CheckSwap(obstacle);
        ObstacleState swappedState = obstacle.state;

        switch(obstacle.state){
            case ObstacleState.Open:{
                if(shouldSwapState) {
                    // Choose between Half or Closed
                    if(UnityEngine.Random.Range(0,2) == 0)
                        swappedState = ObstacleState.Half;
                    else 
                        swappedState = ObstacleState.Blocked;
                    return (true, swappedState);
                } else
                    break;
            }
            case ObstacleState.Half:{
                swappedState = ObstacleState.Open;
                return (true, swappedState);
            }
            case ObstacleState.Blocked:{
                if(shouldSwapState) {
                    swappedState = ObstacleState.Open;
                    return (true, swappedState);
                } else
                    break;
            }
        }
        obstacle.sinceChange++;
        return (false, swappedState);
    }

    private bool CheckSwap(Obstacle obstacle)
    {
        int random = UnityEngine.Random.Range(0, 5);
        if(obstacle.length < obstacle.sinceChange) return false;
        return obstacle.sinceChange > random;
    }

    private void SwapObstacle(Obstacle obstacle, ObstacleState state) 
    {
        obstacle.sinceChange = 0;
        List<Obstacle> obstaclesOfState = statedObstacles[obstacle.state];
        int index = UnityEngine.Random.Range(0, obstaclesOfState.Count);
        obstacle = obstaclesOfState[index];
    }
}
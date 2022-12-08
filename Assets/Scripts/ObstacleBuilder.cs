using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Obstacle 
{
    public GameObject prefab;
    public ObstacleState state;
    public int timesChanged;
    public float length;
}

public enum ObstacleState
{
    Open, Blocked, Half
}

public class ObstacleBuilder : MonoBehaviour
{
    
    ObstacleBuilder()
    {
        statedObstacles = new Dictionary<ObstacleState, List<Obstacle>>();
        foreach (ObstacleState state in Enum.GetValues(typeof(ObstacleState))){
            statedObstacles.Add(state, new List<Obstacle>());
        }
        foreach(Obstacle obs in obstacles){
            statedObstacles[obs.state].Add(obs);
        }
    }

    [SerializeField]
    private Obstacle[] obstacles;

    private Dictionary<ObstacleState, List<Obstacle>> statedObstacles;

    public List<Obstacle> BuildObstacleRow(List<Obstacle> obstacles)
    {
        foreach(Obstacle obstacle in obstacles)
        {
            var result = returnUpdatedState(obstacle);
            if (result.Item1) SwapObstacle(obstacle, result.Item2);
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
                    return (false, swappedState);
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
                    return (false, swappedState);
            }
        }
        return (false, swappedState);
    }

    private bool CheckSwap(Obstacle obstacle)
    {
        int random = UnityEngine.Random.Range(0, 5);
        return obstacle.timesChanged > random;
    }

    private void SwapObstacle(Obstacle obstacle, ObstacleState state) 
    {
        List<Obstacle> obstaclesOfState = statedObstacles[obstacle.state];
        int index = UnityEngine.Random.Range(0, obstaclesOfState.Count);
        obstacle = obstaclesOfState[index];
    }
}
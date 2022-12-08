using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{
    public enum ObstacleState
    {
        Open, Blocked, Half
    }

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

    [Serializable]
    public struct Obstacle 
    {
        public GameObject prefab;
        public ObstacleState state;
        public float length;
    }

    [SerializeField]
    private Obstacle[] obstacles;

    private Dictionary<ObstacleState, List<Obstacle>> statedObstacles;

    public Obstacle[] BuildObstacleRow(Obstacle[] lastRow)
    {
        List<Obstacle> list;

        return new Obstacle[]{};
    }

    private (ObstacleState, bool) returnUpdatedState(Obstacle obstacle){
        switch(obstacle.state){
            case ObstacleState.Open:{
                break;
            }
            case ObstacleState.Half:{
                return (ObstacleState.Open, false);
            }
            case ObstacleState.Blocked:{
                break;
            }
        }
        return (ObstacleState.Open, false);
    }
}
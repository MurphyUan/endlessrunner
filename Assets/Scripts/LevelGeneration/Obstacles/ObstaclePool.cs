using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObstacleWithInstances 
{
    public ObstacleItem Item;
    public List<GameObject> Instances;

    public ObstacleWithInstances(ObstacleItem obs, List<GameObject> obsInstances) : this()
    {
        this.Item = obs;
        this.Instances = obsInstances;
    }
}

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    public List<ObstacleItem> AvailableObstacles;

    public Dictionary<ObstacleState, List<ObstacleWithInstances>> StatedObstacles = new Dictionary<ObstacleState, List<ObstacleWithInstances>>();

    public static ObstaclePool Singleton;

    private void Awake() 
    {
        Singleton = this;

        foreach (ObstacleState state in Enum.GetValues(typeof(ObstacleState)))
        {
            StatedObstacles.Add(state, new List<ObstacleWithInstances>());
        }

        foreach(ObstacleItem obs in AvailableObstacles)
        {
            ObstacleWithInstances obstacle = new ObstacleWithInstances(obs, new List<GameObject>());
            for(int i = 0; i < obs.Length; i++)
            {
                createNewObstacle(obstacle);
            }
            StatedObstacles[obs.State].Add(obstacle);
        }
    }

    public ObstacleWithInstances GetObstacleOfState(ObstacleState state)
    {
        List<ObstacleWithInstances> stateObstacles = StatedObstacles[state];
        Utils.Shuffle(stateObstacles);
        return returnObstacle(stateObstacles[0]);
    }

    public ObstacleWithInstances GetObstacle(ObstacleItem item)
    {
        List<ObstacleWithInstances> stateObstacles = StatedObstacles[item.State];
        ObstacleWithInstances obstacle = stateObstacles.Find(x => x.Item == item);
        return returnObstacle(obstacle);
    }

    private ObstacleWithInstances returnObstacle(ObstacleWithInstances obstacle)
    {
        foreach(GameObject instance in obstacle.Instances)
        {
            if(instance.activeInHierarchy) return new ObstacleWithInstances(obstacle.Item, new List<GameObject>(){instance});
        }
        return createNewObstacle(obstacle);
    }

    private ObstacleWithInstances createNewObstacle(ObstacleWithInstances obstacle)
    {
        GameObject instance = Instantiate(obstacle.Item.Prefab);
        instance.transform.parent = Parent.transform;
        instance.SetActive(false);
        obstacle.Instances.Add(instance);
        return new ObstacleWithInstances(obstacle.Item, new List<GameObject>(){instance});
    }
}

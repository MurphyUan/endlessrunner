using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObstacleWithInstances 
{
    public ObstacleItem Item;
    public List<GameObject> Instances;

    public ObstacleWithInstances(ObstacleItem item, List<GameObject> instances) : this()
    {
        this.Item = item;
        this.Instances = instances;
    }
}

public struct ObstacleWithInstance
{
    public ObstacleItem Item;
    public GameObject Instance;

    public ObstacleWithInstance(ObstacleItem item, GameObject instance) : this()
    {
        this.Item = item;
        this.Instance = instance;
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

    public ObstacleWithInstance GetObstacleOfState(ObstacleState state)
    {
        List<ObstacleWithInstances> stateObstacles = StatedObstacles[state];
        Utils.Shuffle(stateObstacles);
        return returnObstacle(stateObstacles[0]);
    }

    public ObstacleWithInstance GetObstacle(ObstacleItem item)
    {
        List<ObstacleWithInstances> stateObstacles = StatedObstacles[item.State];
        ObstacleWithInstances obstacle = stateObstacles.Find(x => x.Item == item);
        return returnObstacle(obstacle);
    }

    private ObstacleWithInstance returnObstacle(ObstacleWithInstances obstacle)
    {
        foreach(GameObject instance in obstacle.Instances)
        {
            if(instance.activeInHierarchy) return new ObstacleWithInstance(obstacle.Item, instance);
        }
        return createNewObstacle(obstacle);
    }

    private ObstacleWithInstance createNewObstacle(ObstacleWithInstances obstacle)
    {
        GameObject instance = Instantiate(obstacle.Item.Prefab);
        instance.transform.parent = Parent.transform;
        instance.SetActive(false);
        obstacle.Instances.Add(instance);
        return new ObstacleWithInstance(obstacle.Item, instance);
    }
}

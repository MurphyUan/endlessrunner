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

    public GameObject GetObstacleOfState(ObstacleState state)
    {
        List<ObstacleWithInstances> stateObstacles = StatedObstacles[state];
        Utils.Shuffle(stateObstacles);
        foreach(GameObject localInstance in stateObstacles[0].Instances)
        {
            if(localInstance.activeInHierarchy) return localInstance;
        }
        return createNewObstacle(stateObstacles[0]);
    }

    private GameObject createNewObstacle(ObstacleWithInstances obstacle)
    {
        GameObject instance = Instantiate(obstacle.Item.Prefab);
        instance.transform.parent = Parent.transform;
        instance.SetActive(false);
        obstacle.Instances.Add(instance);
        return instance;
    }
}

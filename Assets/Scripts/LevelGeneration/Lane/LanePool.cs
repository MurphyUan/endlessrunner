using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePool : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    public List<GameObject> AvailableLanes;

    public int Amount = 5;

    public Dictionary<GameObject, List<GameObject>> laneDict = new Dictionary<GameObject, List<GameObject>>();

    public static LanePool Singleton;
    
    private void Awake() 
    {
        Singleton = this;    

        foreach(GameObject lane in AvailableLanes)
        {
            List<GameObject> laneObjects = new List<GameObject>();

            for(int i = 0; i < Amount; i++)
            {
                laneObjects.Add(createNewLane(lane.gameObject));
            }

            laneDict.Add(lane, laneObjects);
        }
    }

    public GameObject GetLane(){
        List<GameObject> lanes = new List<GameObject>(laneDict.Keys);
        return returnLane(Utils.GetRandomFromList(lanes));
    }

    private GameObject returnLane(GameObject lane)
    {
        foreach(GameObject instance in laneDict[lane])
        {
            if(!instance.activeInHierarchy) return instance;
        }
        return createNewLane(lane);
    }

    private GameObject createNewLane(GameObject lane)
    {
        GameObject instance = Instantiate(lane);
        instance.transform.parent = Parent.transform;
        instance.SetActive(false);
        return instance;
    }
}

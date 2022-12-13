using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuilder : MonoBehaviour
{
    [SerializeField] private int NumberOfPlatforms = 15;
    [SerializeField] private int NumberOfLanes = 3;
    [SerializeField] private float LaneWidth = 5f;
    [SerializeField] private float StepSize = 8f;

    public GameObject LanePrefab;

    public static GameObject Phantom;
    public static LaneBuilder Singleton;

    private List<ObstacleWithInstance> currentRow = null;

    #region StartUp Methods

    private void Awake()
    {
        Singleton = this;
        
        Phantom = new GameObject("phantom");
    }

    private void Start() 
    {
        for(int i = 0; i < NumberOfPlatforms; i++)
            LaneBuilder.RunPhantom();
    }

    #endregion

    private static void usePhantom()
    {
        List<ObstacleWithInstance> futureRow = ObstacleBuilder.BuildRow(LaneBuilder.Singleton.currentRow, LaneBuilder.Singleton.NumberOfLanes);
        LaneBuilder.Singleton.currentRow = futureRow;

        Phantom.transform.position += GetPositionInDirection(futureRow[0].Instance.transform.position, PlayerBehaviour.Player.transform.forward) +
                PlayerBehaviour.Player.transform.forward * LaneBuilder.Singleton.StepSize;

        Instantiate(LaneBuilder.Singleton.LanePrefab, Phantom.transform.position, Quaternion.identity);

        float startingLeftCoordinate = (LaneBuilder.Singleton.NumberOfLanes / 2) * -(LaneBuilder.Singleton.LaneWidth);

        for(int i = 0; i < LaneBuilder.Singleton.NumberOfLanes; i++)
        {
            GameObject local = futureRow[i].Instance;
            local.transform.position = Phantom.transform.position + PlayerBehaviour.Player.transform.right * startingLeftCoordinate;
            startingLeftCoordinate += LaneBuilder.Singleton.LaneWidth;
            local.SetActive(true);
            Debug.Log($"Position:{local.transform.position}, Active:{local.activeInHierarchy}");
        }
    }

    public static void RunPhantom()
    {
        usePhantom();
    }

    public static Vector3 GetPositionInDirection(Vector3 position, Vector3 direction)
    {
        return new Vector3(position.x * direction.x, position.y * direction.y, position.z * direction.z);
    }
}

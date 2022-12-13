using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuilder : MonoBehaviour
{
    [SerializeField] public int NumberOfPlatforms = 8;
    [SerializeField] private int NumberOfLanes = 3;
    [SerializeField] public float LaneWidth = 5f;
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

    #endregion

    private static void usePhantom()
    {
        List<ObstacleWithInstance> futureRow = ObstacleBuilder.BuildRow(LaneBuilder.Singleton.currentRow, LaneBuilder.Singleton.NumberOfLanes);

        if(LaneBuilder.Singleton.currentRow != null){
            Phantom.transform.position = GetPositionInDirection(LaneBuilder.Singleton.currentRow[0].Instance.transform.position, Phantom.transform.forward);
            Phantom.transform.position += Phantom.transform.forward * LaneBuilder.Singleton.StepSize;
        }

        LaneBuilder.Singleton.currentRow = futureRow;

        Instantiate(LaneBuilder.Singleton.LanePrefab, Phantom.transform.position, Quaternion.identity);

        float startingLeftCoordinate = (LaneBuilder.Singleton.NumberOfLanes / 2) * -(LaneBuilder.Singleton.LaneWidth);

        for(int i = 0; i < LaneBuilder.Singleton.NumberOfLanes; i++)
        {
            GameObject local = futureRow[i].Instance;
            if(futureRow[i].Item.State != ObstacleState.Full){
                // Spawn Coins & Powerups
            }
            local.transform.position = Phantom.transform.position + PlayerBehaviour.Player.transform.right * startingLeftCoordinate;
            startingLeftCoordinate += LaneBuilder.Singleton.LaneWidth;
            local.SetActive(true);
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

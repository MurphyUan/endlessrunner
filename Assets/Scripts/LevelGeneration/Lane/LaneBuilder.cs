using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuilder : MonoBehaviour
{
    [SerializeField] public int NumberOfPlatforms = 8;
    [SerializeField] private int NumberOfLanes = 3;
    [SerializeField] public float LaneWidth = 5f;
    [SerializeField] private float StepSize = 8f;

    public float maxMoveSpeed = 2f;

    public static GameObject Phantom;
    public static LaneBuilder Singleton;

    private static System.Random random = new System.Random();

    private List<ObstacleWithInstance> currentRow = null;
    private GameObject currentLane = null;

    #region StartUp Methods

    private void Awake()
    {
        Singleton = this;
        
        Phantom = new GameObject("phantom");
    }

    #endregion

    public static void RunPhantom(int stat = 1)
    {
        usePhantom(stat);
    }

    private static void usePhantom(int stat = 1)
    {
        List<ObstacleWithInstance> futureRow;
        if(stat == 0)  
            futureRow = ObstacleBuilder.BuildRow(null, LaneBuilder.Singleton.NumberOfLanes);
        futureRow = ObstacleBuilder.BuildRow(LaneBuilder.Singleton.currentRow, LaneBuilder.Singleton.NumberOfLanes);

        if(LaneBuilder.Singleton.currentLane != null)
            MovePhantom();

        GameObject lane = LanePool.Singleton.GetLane();
            lane.transform.position = Phantom.transform.position;
        lane.SetActive(true);

        LaneBuilder.Singleton.currentLane = lane;

        LaneBuilder.Singleton.currentRow = futureRow;

        if(stat == 0)return;

        PlaceObstaclesOnLane(futureRow);
    }

    private static void MovePhantom()
    {
        Phantom.transform.position = GetPositionInDirection(LaneBuilder.Singleton.currentLane.transform.position, Phantom.transform.forward);
            Phantom.transform.position += Phantom.transform.forward * LaneBuilder.Singleton.StepSize;
    }

    private static void PlaceObstaclesOnLane(List<ObstacleWithInstance> futureRow)
    {
        float startingLeftCoordinate = (LaneBuilder.Singleton.NumberOfLanes / 2) * -(LaneBuilder.Singleton.LaneWidth);

        for(int i = 0; i < LaneBuilder.Singleton.NumberOfLanes; i++)
        {
            GameObject local = futureRow[i].Instance;
            if(futureRow[i].Item.State == ObstacleState.Open){
                // Spawn Coins
                if(random.Next(0,8) >= 6) {
                    GameObject coins = CoinPool.Singleton.GetCoinsOfLength(random.Next(1, CoinPool.Singleton.indexCoins.Count));
                    coins.transform.position = Phantom.transform.position + PlayerBehaviour.Player.transform.right * startingLeftCoordinate;
                    foreach(Coin coin in coins.GetComponent<CoinParent>().Coins)
                        coin.gameObject.SetActive(true);
                    coins.SetActive(true);
                }
            }
            local.transform.position = Phantom.transform.position + PlayerBehaviour.Player.transform.right * startingLeftCoordinate;
            startingLeftCoordinate += LaneBuilder.Singleton.LaneWidth;
            local.SetActive(true);
        }
    }

    public static Vector3 GetPositionInDirection(Vector3 position, Vector3 direction)
    {
        return new Vector3(position.x * direction.x, position.y * direction.y, position.z * direction.z);
    }
}

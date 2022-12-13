using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour
{

    public static ObstacleBuilder Singleton;

    private Dictionary<int, List<int>> rowStateChanges;

    

    #region StartUp Methods

    private void Awake()
    {
        Singleton = this;
        rowStateChanges = new Dictionary<int, List<int>>();
        FillDictionary();
    }

    #endregion

    public static List<ObstacleWithInstance> BuildRow(List<ObstacleWithInstance> currentRow, int length)
    {
        return buildRow(currentRow, length);
    }

    private static List<ObstacleWithInstance> buildRow(List<ObstacleWithInstance> currentRow, int length)
    {
        if(currentRow == null) return returnOpenRow(length);

        int stateIndex = 0;
        bool hasHalf = false;

        for(int i = 0; i < currentRow.Count; i++){
            switch(currentRow[i].Item.State){
                case ObstacleState.Full:{
                    stateIndex += (int) Math.Pow(2, i);
                    break;
                }
                case ObstacleState.Half:{
                    hasHalf = true;
                    break;
                }
            }
        }

        return GetNewRow(stateIndex, length, hasHalf);
    }

    private static List<ObstacleWithInstance> GetNewRow (int stateIndex, int length, bool hasHalf)
    {
        List<ObstacleWithInstance> listToBeReturned = new List<ObstacleWithInstance>();
        int newStateIndex = Utils.GetRandomFromList<int>(ObstacleBuilder.Singleton.rowStateChanges[stateIndex]);

        string binaryState = Convert.ToString(newStateIndex, 2);
        for(int i = binaryState.Length; i < length; i++)
            binaryState = "0"+binaryState;

        foreach(char value in binaryState){
            switch(value){
                case '0' when hasHalf:{
                    System.Random random = new System.Random();
                    if(random.Next(0, 5) == 4)
                        listToBeReturned.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Half));
                    else
                        goto case '0';
                    break;
                }
                case '0':{
                    listToBeReturned.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Open));
                    break;
                }
                case '1':{
                    listToBeReturned.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Full));
                    break;
                }
                default:{
                    Debug.Log("Hits Default Somehow");
                    break;
                }
            }
        }

        return listToBeReturned;
    }

    private static bool CheckSwap(ObstacleItem obstacle)
    {
        int random = UnityEngine.Random.Range(0, 6);
        return random > 1;
    }

    private static List<ObstacleWithInstance> returnOpenRow(int length){
        List<ObstacleWithInstance> localArray = new List<ObstacleWithInstance>();
        for(int i = 0; i < length; i++){
            localArray.Add(ObstaclePool.Singleton.GetObstacleOfState(ObstacleState.Open));
        }
        return localArray;
    }

    private void FillDictionary()
    {
        rowStateChanges.Clear();
        rowStateChanges.Add(0, Utils.CreateList<int>(0, 1, 2, 3, 4, 5, 6));
        rowStateChanges.Add(1, Utils.CreateList<int>(0, 1, 2, 3, 4, 5));
        rowStateChanges.Add(2, Utils.CreateList<int>(0, 2));
        rowStateChanges.Add(3, Utils.CreateList<int>(0, 1, 2, 3));
        rowStateChanges.Add(4, Utils.CreateList<int>(0, 1, 2, 4, 5, 6));
        rowStateChanges.Add(5, Utils.CreateList<int>(0, 1, 4, 5));
        rowStateChanges.Add(6, Utils.CreateList<int>(0, 2, 4, 6));
    }
}
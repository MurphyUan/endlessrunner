using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    public List<CoinParent> AvailableCoins;

    public Dictionary<int, List<GameObject>> indexCoins = new Dictionary<int, List<GameObject>>();

    public static CoinPool Singleton;

    private void Awake() {
        Singleton = this;

        foreach(CoinParent parent in AvailableCoins)
        {
            List<GameObject> coins = new List<GameObject>();
            for(int i = 0; i < parent.Amount; i++){
                coins.Add(createNewGameObject(parent.gameObject));
            }
                
            indexCoins.Add(parent.Coins.Count, coins);
        }
    }

    public GameObject GetCoinsOfLength(int index)
    {
        List<GameObject> coins = indexCoins[index];
        return returnGameObject(coins);
    }

    private GameObject returnGameObject(List<GameObject> coins)
    {
        foreach(GameObject instance in coins)
        {
            if(!instance.activeInHierarchy) return instance;
        }
        return createNewGameObject(coins[0]);
    }

    private GameObject createNewGameObject(GameObject coin)
    {
        GameObject instance = Instantiate(coin);
        instance.transform.parent = Parent.transform;
        instance.SetActive(false);
        return instance; 
    }
}

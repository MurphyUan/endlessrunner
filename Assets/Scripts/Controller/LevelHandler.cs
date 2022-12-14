using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private SceneController sceneController;

    private int playerScore = 0;
    private bool shouldCallScroller = false;

    public static bool PlayerDead = false;
    public static bool spawnPlatforms = false;

    private Coroutine scroller;

    private void Start() {
        scroller = StartCoroutine(Scroller.startScroller());

        foreach(int i in Enumerable.Range(0, LaneBuilder.Singleton.NumberOfPlatforms)){
            if(i < 3) LaneBuilder.RunPhantom(0);
            else {
                LaneBuilder.RunPhantom();
                spawnPlatforms = true;
            }
        }
    }

    private void FixedUpdate() {
        if(PlayerDead)return;
        else if(Time.timeScale == 0) {
            StopCoroutine(scroller);
            shouldCallScroller = true;
        }
        else if(shouldCallScroller) {
            scroller = StartCoroutine(Scroller.startScroller());
            shouldCallScroller = false;
        }

        PlayerScore.text = $"Score:{playerScore.ToString("000000000")}";
    }

    public void OnPlayerDeathEvent() {

    }

    public void OnPlayerCoinEvent(Coin coin) {
        playerScore += coin.ScoreValue;
        coin.gameObject.SetActive(false);
    }

    private void OnEnable() {
        Utils.PlayerDeathEvent += OnPlayerDeathEvent;
        Utils.PlayerCoinEvent += OnPlayerCoinEvent;
    }

    private void OnDisable() {
        Utils.PlayerDeathEvent -= OnPlayerDeathEvent;
        Utils.PlayerCoinEvent -= OnPlayerCoinEvent;
    }
}

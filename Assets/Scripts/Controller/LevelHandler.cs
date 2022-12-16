using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private TMP_Text LaneSpeed;
    [SerializeField] private SceneController sceneController;

    [SerializeField] private int StartingLanes = 3;
    [SerializeField] public float maxMoveSpeed = 2;

    public static int playerScore = 0;
    private bool shouldCallScroller = false;

    public static bool PlayerDead = false;
    public static bool spawnPlatforms = false;

    private Coroutine scroller;
    private float _playerScore = 0;

    private void Start() {
        scroller = StartCoroutine(Scroller.startScroller());
        Time.timeScale = 1;

        for(int i = 0; i < LaneBuilder.Singleton.NumberOfPlatforms; i++)
        {
            if(i <= StartingLanes) LaneBuilder.RunPhantom(0);
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
        
        _playerScore += Time.fixedDeltaTime * Scroller.timeElapsed;
        if(_playerScore >= 1){
            _playerScore -= 1;
            playerScore += 1;
        }

        PlayerScore.text = $"Score:{playerScore.ToString()}";
        LaneSpeed.text = $"Speed:{Scroller.moveSpeed}";
    }

    public void OnPlayerDeathEvent() {
        Debug.Log("Player Died");
        PlayerDead = true;
        Time.timeScale = 0;
        sceneController.showRecord();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private SceneController sceneController;

    private  int playerScore = 0;

    private void Start() {
        
    }

    public void OnPlayerDeathEvent() {

    }

    public void OnPlayerCoinEvent(Coin coin) {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreValue;

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = LevelHandler.playerScore.ToString("000000000");
    }

    public void RecordScore(string levelName) {
        LeaderBoard.RecordLevelEntry(levelName, new LeaderBoard.Entry(nameText.text.ToUpper(), LevelHandler.playerScore));
    }
}

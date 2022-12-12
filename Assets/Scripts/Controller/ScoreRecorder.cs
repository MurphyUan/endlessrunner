using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        nameText = GameObject.Find("NameText").GetComponent<TMP_Text>();
        scoreValue = GameObject.Find("ScoreValue").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = 0.ToString("000000000");
    }

    public void RecordScore() {
        LeaderBoard.RecordLevelEntry("", new LeaderBoard.Entry(nameText.text.ToUpper(), 0));
    }
}

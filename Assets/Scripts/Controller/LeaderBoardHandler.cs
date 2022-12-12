using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scores;

    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;

    public List<string> levels;
    private int levelIndex = 0;

    public static LeaderBoardHandler Singleton;

    public static List<string> Levels
    {
        get { return LeaderBoardHandler.Singleton.levels;}
    }

    private void Awake() {
        Singleton = this;
        Debug.Log(Levels);
    }

    public void DisplayScores(int indexChange)
    {
        Debug.Log(levelIndex + indexChange);
        if(levelIndex + indexChange >=  Levels.Count || levelIndex + indexChange < 0){
            scores.text = "Level Does not Exist";
            return;
        }

        levelIndex += indexChange;

        if(levelIndex <= 0)
            LeftArrow.SetActive(false);
        else if (levelIndex > 0)
            LeftArrow.SetActive(true);

        if(levelIndex >= Levels.Count - 1)
            RightArrow.SetActive(false);
        else if(levelIndex < Levels.Count - 1)
            RightArrow.SetActive(true);

        scores.text = LeaderBoard.DisplayLevelEntries(Levels[levelIndex]);
    }
}

// Taken from previously completed project, altered to work with multiple levels
public static class LeaderBoard
{
    public const int numEntries = 10;

    public struct Entry 
    {
        public string name;
        public int score;

        public Entry (string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }


    private static Dictionary<string, List<Entry>> levelEntries;

    private static Dictionary<string, List<Entry>> LevelEntries
    {
        get {
            if (levelEntries == null && LeaderBoardHandler.Levels != null) {
                levelEntries = new Dictionary<string, List<Entry>>();
                foreach(string level in LeaderBoardHandler.Levels){
                    LoadLevelEntries(level);
                }
            }
            return levelEntries;
        }
    }

    private const string PlayerPrefsKey = "leaderboard";

    private static void SortLevelEntries(string levelName)
    {
        levelEntries[levelName].Sort((a, b) => b.score.CompareTo(a.score));
    }

    private static void LoadLevelEntries(string levelName)
    {
        if(!levelEntries.ContainsKey(levelName))
            levelEntries.Add(levelName, new List<Entry>());
        
        levelEntries[levelName].Clear();

        for(int i = 0; i < numEntries; i++)
        {
            Entry entry;
            entry.name = PlayerPrefs.GetString($"{PlayerPrefsKey}.{levelName}[{i}].name", "AAA");
            entry.score = PlayerPrefs.GetInt($"{PlayerPrefsKey}.{levelName}[{i}].score", 0);
            levelEntries[levelName].Add(entry);
        }
        SortLevelEntries(levelName);
    }

    private static void SaveLevelEntries(string levelName)
    {
        foreach(Entry entry in levelEntries[levelName]){
            PlayerPrefs.SetString($"{PlayerPrefsKey}.{levelName}[{levelEntries[levelName].IndexOf(entry)}].name", entry.name);
            PlayerPrefs.SetInt($"{PlayerPrefsKey}.{levelName}[{levelEntries[levelName].IndexOf(entry)}].score", entry.score);
            PlayerPrefs.Save();
        }
    }

    public static Entry GetEntry(string levelName, int index)
    {
        return LevelEntries[levelName][index];
    }

    public static void RecordLevelEntry(string levelName, Entry entry)
    {
        LevelEntries[levelName].Add(entry);
        SortLevelEntries(levelName);
        LevelEntries[levelName].RemoveAt(LevelEntries.Count - 1);
        SaveLevelEntries(levelName);
    }

    public static string DisplayLevelEntries(string levelName)
    {
        if(!LevelEntries.ContainsKey(levelName))
            LoadLevelEntries(levelName);

        string value = $"{levelName}\n";

        foreach(Entry entry in LevelEntries[levelName])
            value += entry.name + " - " + entry.score.ToString("000000000") + "\n";
        return value;
    }
}

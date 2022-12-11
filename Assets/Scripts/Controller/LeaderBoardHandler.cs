using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scores;
    public List<string> levels;

    public static LeaderBoardHandler Singleton;

    public static List<string> Levels
    {
        get { return LeaderBoardHandler.Singleton.levels;}
    }

    private void Awake() {
        Singleton = this;
    }

    public void UpdateScores(string levelName)
    {
        scores.text = LeaderBoard.DisplayLevelEntries(levelName);
    }
}

// Taken from previously completed project
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
        levelEntries.Clear();

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
        string value = "";
        foreach(Entry entry in LevelEntries[levelName])
            value += entry.name + " - " + entry.score.ToString("000000000") + "\n";
        return value;
    }
}

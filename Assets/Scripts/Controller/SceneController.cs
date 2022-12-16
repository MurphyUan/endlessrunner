using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverlay;
    [SerializeField] private GameObject MenuOverlay;
    [SerializeField] private GameObject EndOverlay;

    public static SceneController Singleton;

    private bool showMenu = false;

    private void Awake()
    {
        Singleton = this;
    }

    public void NavigateToScene(string sceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != sceneName)
        {
            scene = SceneManager.GetSceneByName(sceneName);
            if(scene != null)
                SceneManager.LoadSceneAsync(sceneName);
            return;
        }
        Debug.Log("Already in Scene");
    }

    public static void NavigateToNextInSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void showRecord()
    {
        GameOverlay.SetActive(false);
        MenuOverlay.SetActive(false);
        EndOverlay.SetActive(true);
    }

    public void UpdateVisibility() 
    {
        showMenu = !showMenu;
        if(showMenu) Time.timeScale = 0;
        else Time.timeScale = 1;
        GameOverlay.SetActive(!showMenu);
        MenuOverlay.SetActive(showMenu);
    }

    public void RecordScore(TMP_Text nameText) 
    {
        LeaderBoard.RecordLevelEntry(SceneManager.GetActiveScene().name, new LeaderBoard.Entry(nameText.name, LevelHandler.playerScore));
    }
}

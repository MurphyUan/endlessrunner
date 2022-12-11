using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<GameObject> overlays;

    private bool showMenu = false;

    private void Update() 
    {
        // Update Visibility of Overlays
    }

    public void NavigateToScene(string sceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != sceneName)
        {
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
}

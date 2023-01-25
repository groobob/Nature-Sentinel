using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class for managing the loading of scenes
 */

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    // Singleton
   private void Awake()
    {
        Instance = this;
    }

    // Loads the start scene
    public void LoadStartScene()
    {
        if (MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Title Scene");
    }

    // Loads the tutorial scene
    public void LoadTutorialScene()
    {
        if(MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Tutorial Scene");
    }

    // Loads the gameplay scene
    public void LoadGameScene()
    {
        if (MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Game Scene");
    }

    // Loads the game over scene
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over Scene");
    }
}

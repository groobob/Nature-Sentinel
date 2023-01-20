using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
   private void Awake()
    {
        Instance = this;
    }
    public void LoadStartScene()
    {
        if (MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Title Scene");
    }
    public void LoadTutorialScene()
    {
        if(MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Tutorial Scene");
    }
    public void LoadGameScene()
    {
        if (MenuManager.GetScore() != 0) MenuManager.SetScore(0);
        SceneManager.LoadScene("Game Scene");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over Scene");
    }
}

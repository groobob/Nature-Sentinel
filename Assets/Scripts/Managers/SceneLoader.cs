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
        SceneManager.LoadScene("Title Scene");
    }
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over Scene");
    }
}

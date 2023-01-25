using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class made for displaying score on a different scene
 */

public class ShowScore : MonoBehaviour
{
    [SerializeField] Text scoreText;

    // Loads the score to a specified text box
    private void Start()
    {
        scoreText.text = "Final Score: " + MenuManager.GetScore().ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private void Start()
    {
        scoreText.text = "Final Score: " + MenuManager.GetScore().ToString();
    }
}

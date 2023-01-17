using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour
{
    [SerializeField] public ScriptableCard card;

    [SerializeField] public Sprite cardBorder;

    [SerializeField] public Text nameText;
    [SerializeField] public Text descriptionText;
    [SerializeField] public Text damageText;
    [SerializeField] public Text rangeText;

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (GameManager.Instance.State != GameState.PlayerTurn) return;

        if (UnitManager.Instance.SelectedPlayer != null)
        {
            CardManager.Instance.SetSelectedCard(this);
        }
    }

}

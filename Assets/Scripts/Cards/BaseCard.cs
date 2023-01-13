using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    [SerializeField] public string cardName;
    [SerializeField] public int damage;
    [SerializeField] public int range;
    [SerializeField] public int speed;
    [SerializeField] public GameObject shotPrefab;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject selectedPlayerObject, tileObject, tileUnitObject, selectedCardObject, endTurnButton;
    public static int score = 0;

    void Awake()
    {
        Instance = this;
    }

    public void ShowTileInfo(Tile tile)
    {
        if(tile == null)
        {
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }

        tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        tileObject.SetActive(true);

        if(tile.OccupiedUnit)
        {
            tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedPlayer(BasePlayer player)
    {
        if(player == null)
        {
            selectedPlayerObject.SetActive(false);
            return;
        }
        selectedPlayerObject.GetComponentInChildren<Text>().text = player.UnitName;
        selectedPlayerObject.SetActive(true);
    }

    public void ShowSelectedCard(BaseCard card)
    {
        if(card == null)
        {
            selectedCardObject.SetActive(false);
            return;
        }
        selectedCardObject.GetComponentInChildren<Text>().text = card.card.cardName;
        selectedCardObject.SetActive(true);

    }

    public void ShowEndTurnButton()
    {
        endTurnButton.SetActive(true);
    }

    public void EndTurn()
    {
        UnitManager.Instance.SetHasMoved(false);
        UnitManager.Instance.SetSelectedPlayer(null);
        CardManager.Instance.SetCanShoot(true);
        CardManager.Instance.SetHasShot(false);
        CardManager.Instance.SetSelectedCard(null);
        endTurnButton.SetActive(false);
        StartCoroutine(Coroutine());
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }

    public void AddScore(int n)
    {
        score += n;
    }

    public static void SetScore(int n)
    {
        score = n;
    }
    public static int GetScore()
    {
        return score;
    }
}

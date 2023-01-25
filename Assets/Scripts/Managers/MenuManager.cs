using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class for managing selection and menu information
 * Also has functionality for ending turns
 */

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject selectedPlayerObject, tileObject, tileUnitObject, selectedCardObject, endTurnButton;
    public static int score = 0;

    // Singleton
    void Awake()
    {
        Instance = this;
    }

    // Returns information of hovering tile and enters the information to a text box
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

    // Displays the selected player on a text box
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

    // Displays the current selected card on a text box
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

    // Displays the button that is used to end the turn
    public void ShowEndTurnButton()
    {
        endTurnButton.SetActive(true);
    }

    // Called to end the current player turn, switches to enemy turn
    public void EndTurn()
    {
        UnitManager.Instance.SetHasMoved(false);
        UnitManager.Instance.SetSelectedPlayer(null);
        CardManager.Instance.SetCanShoot(true);
        CardManager.Instance.SetHasShot(false);
        CardManager.Instance.SetSelectedCard(null);
        endTurnButton.SetActive(false);
        foreach (Tile tile in Tile.proximityTiles)
        {
            tile.movementHighlight.SetActive(false);
        }
        Tile.proximityTiles.Clear();
        StartCoroutine(Coroutine());
    }

    // Coroutine used as a timer between turns
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }

    // Adds a certain amount of score to the player score
    public void AddScore(int n)
    {
        score += n;
    }

    // Sets the score to an inputted value
    public static void SetScore(int n)
    {
        score = n;
    }

    // Returns the current score
    public static int GetScore()
    {
        return score;
    }
}

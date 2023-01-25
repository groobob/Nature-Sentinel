using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Manages how all card data is stored
 * Also contains information for card generation
 */

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<ScriptableCard> _cards;
    public BaseCard SelectedCard;
    public bool canShoot = false;
    public bool hasShot = false;

    public BaseCard card;
    public Canvas canvas;
    public List<BaseCard> cardQueue = new List<BaseCard>();
    private Vector3 cardPosition1 = new Vector3(350, -65);
    private Vector3 cardPosition2 = new Vector3(350, -195);
    private Vector3 cardPosition3 = new Vector3(255, -195);
    private Vector3 nextCardPosition = new Vector3(-350, -195);

    private BaseCard generatedCard;

    // Runs onstart up and sets all loaded cards to their designated positions on the canvas, also sets this as a singleton
    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCard>("cards").ToList();
        generatedCard = Instantiate(card, transform.position, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        generatedCard.transform.localPosition = cardPosition1;
        cardQueue.Add(generatedCard);

        generatedCard = Instantiate(card, transform.position, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard.transform.localPosition = cardPosition2;

        generatedCard = Instantiate(card, transform.position, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard.transform.localPosition = cardPosition3;

        generatedCard = Instantiate(card, transform.position, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard.transform.localPosition = nextCardPosition;
    }

    // Called when a card is destroyed/used to update the next card and card queue
    public void UpdateNewCardQueue()
    {
        generatedCard = Instantiate(card, nextCardPosition, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard.transform.localPosition = nextCardPosition;

        cardQueue[0].transform.localPosition = cardPosition1;
        cardQueue[1].transform.localPosition = cardPosition2;
        cardQueue[2].transform.localPosition = cardPosition3;
        cardQueue[3].transform.localPosition = nextCardPosition;
    }

    // Returns a random card scriptable object
    public ScriptableCard GetRandomCard()
    {
        return _cards.OrderBy(o => Random.value).First();
    }

    // Removes a certain card from the list/queue of cards
    public void RemoveCardFromQueue(BaseCard card)
    {
        cardQueue.Remove(card);
    }

    // Sets the given card as the selected card
    public void SetSelectedCard(BaseCard card)
    {
        SelectedCard = card;
        if(SelectedCard != null) canShoot = true;
    }

    // Modifies the canShoot variable to the given input
    public void SetCanShoot(bool inputBool)
    {
        canShoot = inputBool;
    }

    // Modifies the hasShot variable to the given input
    public void SetHasShot(bool inputBool)
    {
        hasShot = inputBool;
    }
}

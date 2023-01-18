using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<ScriptableCard> _cards;
    public BaseCard SelectedCard;
    public bool canShoot = false;

    public BaseCard card;
    public Canvas canvas;
    public List<BaseCard> cardQueue = new List<BaseCard>();
    private Vector3 cardPosition1 = new Vector3(1310, 515);
    private Vector3 cardPosition2 = new Vector3(1310, 385);
    private Vector3 cardPosition3 = new Vector3(1220, 385);
    private Vector3 nextCardPosition = new Vector3(610, 385);

    private BaseCard generatedCard;
    public ScriptableCard nextCard;

    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCard>("cards").ToList();
        nextCard = GetRandomCard();
        generatedCard = Instantiate(card, cardPosition1, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard = Instantiate(card, cardPosition2, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard = Instantiate(card, cardPosition3, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
        generatedCard = Instantiate(card, nextCardPosition, Quaternion.identity, canvas.transform);
        generatedCard.card = GetRandomCard();
        cardQueue.Add(generatedCard);
    }

    public void UpdateNewCardQueue()
    {
        generatedCard = Instantiate(card, nextCardPosition, Quaternion.identity, canvas.transform);
        generatedCard.card = nextCard;
        cardQueue.Add(generatedCard);
        nextCard = GetRandomCard();
        cardQueue[0].transform.position = cardPosition1;
        cardQueue[1].transform.position = cardPosition2;
        cardQueue[2].transform.position = cardPosition3;
        cardQueue[3].transform.position = nextCardPosition;
    }

    public ScriptableCard GetRandomCard()
    {
        return _cards.OrderBy(o => Random.value).First();
    }

    public void RemoveCardFromQueue(BaseCard card)
    {
        cardQueue.Remove(card);
    }
    public void SetSelectedCard(BaseCard card)
    {
        SelectedCard = card;
        CardManager.Instance.canShoot = true;
    }
}

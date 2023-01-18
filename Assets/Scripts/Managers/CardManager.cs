using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void SetCanShoot(bool inputBool)
    {
        canShoot = inputBool;
    }

    public void SetHasShot(bool inputBool)
    {
        hasShot = inputBool;
    }
}

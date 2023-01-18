using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<ScriptableCard> _cards;
    public BaseCard SelectedCard;
    public bool canShoot = false;

    public Vector3 cardPosition1 = new Vector3(350, -25);
    public Vector3 cardPosition2 = new Vector3(350, -155);
    public Vector3 cardPosition3 = new Vector3(260, -155);
    public Vector3 nextCardPosition = new Vector3(-350, -155);

    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCard>("cards").ToList();
    }

    public BaseCard GetRandomCard()
    {
        return _cards.OrderBy(o => Random.value).First().CardPrefab;
    }

    public void SetSelectedCard(BaseCard card)
    {
        SelectedCard = card;
        CardManager.Instance.canShoot = true;
    }
}

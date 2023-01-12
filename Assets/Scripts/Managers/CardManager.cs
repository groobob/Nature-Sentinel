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
    }
}

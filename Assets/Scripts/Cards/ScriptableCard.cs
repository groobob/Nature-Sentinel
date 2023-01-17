using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Card")]

public class ScriptableCard : ScriptableObject
{
    public BaseCard CardPrefab;
    public string cardName;
    public string description;
    public int damage;
    public int range;
    public int speed;
    public GameObject shotPrefab;
}

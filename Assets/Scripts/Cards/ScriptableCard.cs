using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Card")]

public class ScriptableCard : ScriptableObject
{
    public BaseCard CardPrefab;
}

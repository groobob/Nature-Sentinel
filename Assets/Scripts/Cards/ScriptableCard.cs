using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Card")]

public class ScriptableCard : ScriptableObject
{
    public GameObject shotPrefab;
    public GameObject deathParticle;
    public Sprite cardImage;
    public string cardName;
    public string description;
    public int damage;
    public float range;
    public int speed;
}

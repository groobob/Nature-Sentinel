using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Stores general information for each unit in the entire game
 */

public class BaseUnit : MonoBehaviour
{
    public string UnitName;
    public Tile occupiedTile;
    public Faction faction;
}

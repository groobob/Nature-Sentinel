using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    [SerializeField] private int moveRange;
    PathFinder finder = new PathFinder();
}

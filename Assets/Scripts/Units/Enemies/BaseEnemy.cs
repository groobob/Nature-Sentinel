using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    [SerializeField] private int moveRange;
    [SerializeField] public int health;
    PathFinder finder = new PathFinder();
    private Tile currentTile;


    public void Move()
    {
        currentTile = GridManager.Instance.tiles[transform.position];
        List<Tile> path = finder.FindPath(currentTile, UnitManager.Instance.playerTile);
        Debug.Log(currentTile);
        Debug.Log(UnitManager.Instance.playerTile);
        Debug.Log(moveRange);
        Debug.Log(path);
        path[moveRange].SetUnit(this);
        Debug.Log("Past!");
    }

}

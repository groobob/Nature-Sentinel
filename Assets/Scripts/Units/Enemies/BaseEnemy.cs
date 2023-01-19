using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    [SerializeField] private int moveRange;
    [SerializeField] public int health;
    [SerializeField] public int score;
    PathFinder finder = new PathFinder();
    private Tile currentTile;


    public void Move()
    {
        currentTile = GridManager.Instance.tiles[transform.position];
        List<Tile> path = finder.FindPath(currentTile, UnitManager.Instance.playerTile);
        if(path.Count != 0)
        {
            if (path[moveRange - 1].OccupiedUnit != null)
            {
                if (path[moveRange - 1].OccupiedUnit.faction == Faction.Player) SceneLoader.Instance.LoadGameOverScene();
            }
            path[moveRange - 1].SetUnit(this);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            UnitManager.Instance.enemies.Remove(this);
            Destroy(gameObject);
        }
        if (UnitManager.Instance.enemies.Count == 0)
        {
            GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
    }

}

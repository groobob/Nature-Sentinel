using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles all the behaviour for all enemies
 * Also stores the common statistics for each enemy
 */

public class BaseEnemy : BaseUnit
{
    [SerializeField] private int moveRange;
    [SerializeField] public int health;
    [SerializeField] public int scoreReward;
    PathFinder finder = new PathFinder();
    private Tile currentTile;

    // Moves the enemy closer to the player by a predetermined amount of spaces
    public void Move()
    {
        currentTile = GridManager.Instance.tiles[transform.position];
        List<Tile> path = finder.FindPath(currentTile, UnitManager.Instance.playerTile);
        if (path.Count > 0)
        {
            if (path.Count >= moveRange)
            {
                if (path[moveRange - 1].OccupiedUnit != null)
                {
                    if (path[moveRange - 1].OccupiedUnit.faction == Faction.Player) SceneLoader.Instance.LoadGameOverScene();
                }
                path[moveRange - 1].SetUnit(this);
            }
            else
            {
                if (path[0].OccupiedUnit != null)
                {
                    if (path[0].OccupiedUnit.faction == Faction.Player) SceneLoader.Instance.LoadGameOverScene();
                }
                path[0].SetUnit(this);
            }
        }  
    }

    // Makes the enemy take a specified amount of damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            MenuManager.Instance.AddScore(scoreReward);
            UnitManager.Instance.enemies.Remove(this);
            Destroy(gameObject);
        }
        if (UnitManager.Instance.enemies.Count == 0)
        {
            GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
    }

}

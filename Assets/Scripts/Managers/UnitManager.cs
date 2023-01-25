using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Manages the information for all units on the map
 * Also has functionality for spawning enemies and players
 */

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

    public BasePlayer SelectedPlayer;
    public Tile playerTile;
    public List<BaseEnemy> enemies;

    public int enemyCount;
    public int enemyIncrease;

    public bool hasMoved;

    // Singleton and loads all scritable unit objects to a list
    private void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("units").ToList();
    }

    // Spawns players on the grid
    public void SpawnPlayers()
    {
        var playerCount = 1;

        for(int i = 0; i < playerCount; i++)
        {
            var randomPrefab = GetRandomUnit<BasePlayer>(Faction.Player);
            var spawnedPlayer = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetPlayerSpawnTile();

            randomSpawnTile.SetUnit(spawnedPlayer);
            playerTile = randomSpawnTile;

            GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
    }

    // Spawns randomly generated enemies on the grid
    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            enemies.Add(spawnedEnemy);
            randomSpawnTile.SetUnit(spawnedEnemy);

            GameManager.Instance.ChangeState(GameState.PlayerTurn);
        }
        SetHasMoved(false);
        CardManager.Instance.SetCanShoot(true);
        CardManager.Instance.SetHasShot(false);

        enemyCount += enemyIncrease;
    }

    // Sets the "hasMoved" variable to whatever inputted
    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }


    // Returns a random unit by faction type
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    // Sets the current selected player to the input
    public void SetSelectedPlayer(BasePlayer player)
    {
        SelectedPlayer = player;
        MenuManager.Instance.ShowSelectedPlayer(player);
    }

    // Sets the tile that the player is currently on
    public void SetPlayerTile(Tile tile)
    {
        playerTile = tile;
    }
}

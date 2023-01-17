using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("units").ToList();
    }

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
        enemyCount += enemyIncrease;
    }

    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedPlayer(BasePlayer player)
    {
        SelectedPlayer = player;
        MenuManager.Instance.ShowSelectedPlayer(player);
    }

    public void SetPlayerTile(Tile tile)
    {
        playerTile = tile;
    }
}

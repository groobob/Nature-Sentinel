using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Manages the grid and its generation behaviour
 * Also gives information about enemy/player spawn locations
 */

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, treeTile, deadTile;
    [SerializeField] private Transform transformCamera;
    public Dictionary<Vector2, Tile> tiles;

    // Singleton
    private void Awake()
    {
        Instance = this;
    }

    // Generates the grid with specified x and y when called, also sets the camera position to the middle of the grid
    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var randomTile = Random.Range(0, 5) == 3 ? treeTile : grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"tile {x} {y}";
                spawnedTile.GetComponent<Tile>().gridLocation = new Vector3(x, y);

                spawnedTile.init(x, y);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        transformCamera.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f);

        GameManager.Instance.ChangeState(GameState.SpawnPlayers);
    }
    
    // Returns a random possible location for the player
    public Tile GetPlayerSpawnTile()
    {
        return tiles.Where(t => t.Key.y < height && t.Value.walkable).OrderBy(tag => Random.value).First().Value;
    }

    // Returns a random possible location for the enemy
    public Tile GetEnemySpawnTile()
    {
        return tiles.Where(t => t.Key.y < height && t.Value.walkable).OrderBy(tag => Random.value).First().Value;
    }

    // Returns a tile object at a given location
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    // Returns a prefab of a dead tile object
    public Tile GetDeadTile()
    {
        return deadTile;
    }
}

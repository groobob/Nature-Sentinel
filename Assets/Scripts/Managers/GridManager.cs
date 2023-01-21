using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, treeTile;
    [SerializeField] private Transform transformCamera;
    public Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        Instance = this;
    }

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
    
    public Tile GetPlayerSpawnTile()
    {
        return tiles.Where(t => t.Key.y < height && t.Value.walkable).OrderBy(tag => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return tiles.Where(t => t.Key.y < height && t.Value.walkable).OrderBy(tag => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}

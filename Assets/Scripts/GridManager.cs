using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Class Comment
*/
public class GridManager : MonoBehaviour
{
    //Reference Variables
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, mountainTile;
    [SerializeField] private Transform transformCamera;
    private Dictionary<Vector2, Tile> tiles;

    void Start()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"tile {x} {y}";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.SetOffsetColor(isOffset);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        transformCamera.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}

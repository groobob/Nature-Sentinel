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
    [SerializeField] private Tile gridTile;
    [SerializeField] private Transform transformCamera;


    void Start()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(gridTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"tile {x} {y}";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.SetOffsetColor(isOffset);
            }
        }

        transformCamera.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f);
    }
}

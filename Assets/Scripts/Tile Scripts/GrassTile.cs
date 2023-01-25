using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Inherits from the tile class for any special functionality
 */

public class GrassTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;
    
    public override void init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        spriteRenderer.color = isOffset ? baseColor : offsetColor;
    }
}

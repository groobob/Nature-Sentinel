using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    public override void init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        spriteRenderer.color = isOffset ? baseColor : offsetColor;
    }
}

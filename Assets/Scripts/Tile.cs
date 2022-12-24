using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Class comment
*/
public class Tile : MonoBehaviour
{
    //Reference Variables
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetOffsetColor(bool offset)
    {
        if(!offset) spriteRenderer.color = baseColor;
        else if(offset) spriteRenderer.color = offsetColor;
    }
}

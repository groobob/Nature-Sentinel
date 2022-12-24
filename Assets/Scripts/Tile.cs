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
    [SerializeField] private GameObject highlight;

    public void SetOffsetColor(bool offset)
    {
        if(!offset) spriteRenderer.color = baseColor;
        else if(offset) spriteRenderer.color = offsetColor;
    }
    
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}

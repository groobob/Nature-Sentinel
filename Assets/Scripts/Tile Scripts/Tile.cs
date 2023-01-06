using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Class comment
*/
public abstract class Tile : MonoBehaviour
{
    //Reference Variables
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    public virtual void init(int x, int y)
    {
        
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

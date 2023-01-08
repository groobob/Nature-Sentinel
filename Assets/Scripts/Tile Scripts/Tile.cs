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
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool walkable => isWalkable && OccupiedUnit == null;

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

    public void SetUnit(BaseUnit unit)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.occupiedTile = this;
    }
}

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

    void OnMouseDown()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn) return;

        //attack
        if(OccupiedUnit != null)
        {
            if (OccupiedUnit.faction == Faction.Player) UnitManager.Instance.SetSelectedPlayer((BasePlayer)OccupiedUnit);
            else
            {
                if (UnitManager.Instance.SelectedPlayer != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    //attacking logic (decrease health or kill enemy)
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SelectedPlayer = null;
                }
            }
        }
        //move
        else
        {
            if(UnitManager.Instance.SelectedPlayer != null)
            {
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.occupiedTile = this;
    }
}

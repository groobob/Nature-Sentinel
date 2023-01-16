using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
/*
    Class comment
*/
public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private bool isWalkable;

    public string TileName;

    public BaseUnit OccupiedUnit;
    
    public bool walkable => isWalkable && OccupiedUnit == null;

    public int G;
    public int H;

    public int F { get { return G + H; } }

    public Tile previous;
    public Vector3Int gridLocation;

    //for pathfinding
    public int distance;

    public virtual void init(int x, int y)
    {
        
    }

    void OnMouseEnter()
    {
        if(!highlight.activeInHierarchy) highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }
    void OnMouseExit()
    {
        if (highlight.activeInHierarchy) highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn) return;

        //attack
        if(OccupiedUnit != null)
        {
            if (OccupiedUnit.faction == Faction.Player)
            {
                UnitManager.Instance.SetSelectedPlayer((BasePlayer)OccupiedUnit);
            }
            else if (OccupiedUnit.faction == Faction.Enemy && CardManager.Instance.canShoot)
            {
                if (UnitManager.Instance.SelectedPlayer != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    UnitManager.Instance.SelectedPlayer.ShootBulletAtMouse(CardManager.Instance.SelectedCard);
                    UnitManager.Instance.SelectedPlayer = null;
                    UnitManager.Instance.SetHasMoved(false);
                    MenuManager.Instance.ShowSelectedPlayer(null);
                    //placeholder, make show end turn buttom in menu manager
                    GameManager.Instance.ChangeState(GameState.EnemyTurn);
                }
            }
        }
        //move
        else
        {
            if(UnitManager.Instance.SelectedPlayer != null && walkable && !CardManager.Instance.canShoot && !UnitManager.Instance.hasMoved)
            {
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);
                UnitManager.Instance.SetHasMoved(true);
                UnitManager.Instance.SetPlayerTile(this);
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

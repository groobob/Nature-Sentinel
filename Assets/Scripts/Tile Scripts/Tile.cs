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

    public float G;
    public float H;

    public float F { get { return G + H; } }

    public Tile previous;
    public Vector2 gridLocation;

    //for pathfinding
    public int distance;
    public PathFinder finder = new PathFinder();
    public List<Tile> proximityTiles;

    public void Awake()
    {
        gridLocation = new Vector2(transform.position.x, transform.position.y);
    }
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
                    MenuManager.Instance.ShowEndTurnButton();
                }
            }
        }
        //move
        else
        {
            if (UnitManager.Instance.SelectedPlayer != null)
            {
                proximityTiles = finder.GetProximityTiles(UnitManager.Instance.playerTile, UnitManager.Instance.SelectedPlayer.moveDistance);
                if (walkable && !CardManager.Instance.canShoot && !UnitManager.Instance.hasMoved && proximityTiles.Contains(this))
                {
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);
                    UnitManager.Instance.SetHasMoved(true);
                    UnitManager.Instance.SetPlayerTile(this);
                    MenuManager.Instance.ShowEndTurnButton();
                }
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

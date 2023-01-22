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
    [SerializeField] private GameObject movementHighlight;
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
    public static List<Tile> proximityTiles;

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

        if(OccupiedUnit != null)
        {
            //select
            if (OccupiedUnit.faction == Faction.Player)
            {
                UnitManager.Instance.SetSelectedPlayer((BasePlayer)OccupiedUnit);
                if (!UnitManager.Instance.hasMoved)
                {
                    proximityTiles = finder.GetProximityTiles(UnitManager.Instance.playerTile, UnitManager.Instance.SelectedPlayer.moveDistance);
                    foreach (Tile tile in proximityTiles) 
                    { 
                        tile.movementHighlight.SetActive(true);
                    }
                }
            }
            //attack
            else if (OccupiedUnit.faction == Faction.Enemy && CardManager.Instance.canShoot && UnitManager.Instance.SelectedPlayer != null)
            {
                var enemy = (BaseEnemy)OccupiedUnit;
                UnitManager.Instance.SelectedPlayer.ShootBulletAtMouse(CardManager.Instance.SelectedCard);
                UnitManager.Instance.SelectedPlayer = null;
                CardManager.Instance.canShoot = false;
                MenuManager.Instance.ShowSelectedPlayer(null);
                MenuManager.Instance.ShowEndTurnButton();
                foreach (Tile tile in proximityTiles)
                {
                    tile.movementHighlight.SetActive(false);
                }
                proximityTiles.Clear();
            }
        }
        //move
        else
        {
            if (UnitManager.Instance.SelectedPlayer != null && walkable && !UnitManager.Instance.hasMoved && proximityTiles.Contains(this))
            {
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);
                UnitManager.Instance.SetHasMoved(true);
                UnitManager.Instance.SetPlayerTile(this);
                MenuManager.Instance.ShowEndTurnButton();
                foreach (Tile tile in proximityTiles)
                {
                    tile.movementHighlight.SetActive(false);
                }
                proximityTiles.Clear();
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

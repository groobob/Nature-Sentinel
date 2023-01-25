using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/*
 * Handles everything to do with any sort of tile and its child classes
 * Also has pathfinding implementations included
 */

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] public GameObject movementHighlight;
    [SerializeField] private bool isWalkable;

    public string TileName;

    public BaseUnit OccupiedUnit;
    
    public bool walkable => isWalkable && OccupiedUnit == null;

    // Varibles for pathfinding
    public float G;
    public float H;
    public float F { get { return G + H; } }

    public Tile previous;
    public Vector2 gridLocation;

    public int distance;
    public PathFinder finder = new PathFinder();
    public static List<Tile> proximityTiles;

    // Stores the location of this current tile as an accessible variable
    public void Awake()
    {
        gridLocation = new Vector2(transform.position.x, transform.position.y);
    }

    // Initialize function ment to be overriden by other functions
    public virtual void init(int x, int y)
    {
        
    }

    // Activates a highlight when you mouse over this tile
    void OnMouseEnter()
    {
        if(!highlight.activeInHierarchy) highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    // Deactivates a highlight when you mouse leaves this tile
    void OnMouseExit()
    {
        if (highlight.activeInHierarchy) highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    // Handles all the logic for whenever you would click a tile
    void OnMouseDown()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn) return;

        if(OccupiedUnit != null)
        {
            // Selection of player
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
            // Attacking an enemy
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
        // Moving a selected player
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

    // Sets an inputted unit to this current tile's location and updates variables
    public void SetUnit(BaseUnit unit)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.occupiedTile = this;
    }
}

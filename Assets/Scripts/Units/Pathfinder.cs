using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * A* Pathfinding algorithm applied for this projects grid
 */

public class PathFinder
{
    // Finds and returns a list of tiles that are the path from the specified start to end location
    public List<Tile> FindPath(Tile start, Tile end)
    {
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(start);

        while(openList.Count > 0)
        {
            Tile currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if(currentTile == end)
            {
                return GetFinishedList(start, end);
            }

            foreach (var neighbour in GetNeighbourTiles(currentTile))
            {
                if(!neighbour.walkable || closedList.Contains(neighbour))
                {
                    if (!(neighbour.OccupiedUnit != null && neighbour.OccupiedUnit.faction == Faction.Player)) continue;
                }

                neighbour.G = GetManhattanDistance(start, neighbour);
                neighbour.H = GetManhattanDistance(end, neighbour);

                neighbour.previous = currentTile;

                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<Tile>();
    }

    // Gets the accessable tiles from a start point to a specified distance
    public List<Tile> GetProximityTiles(Tile start, int distance)
    {
        List<Tile> openList = new List<Tile>();

        start.distance = 0;
        openList.Add(start);

        int currentIndex = 0;
        Tile currentTile;

        while (openList.Count > currentIndex)
        {
            currentTile = openList[currentIndex];

            if (currentTile.distance == distance) break;

            int newDistance = currentTile.distance + 1;

            foreach (Tile tile in GetNeighbourTiles(currentTile))
            {
                if (!openList.Contains(tile) && tile.walkable)
                {
                    openList.Add(tile);
                    tile.distance = newDistance;
                }
            }

            currentIndex++;
        }
        return openList;
    }

    // Returns a finished list of tiles as the path from a specified start to end from using the information from "FindPath()"
    private List<Tile> GetFinishedList(Tile start, Tile end)
    {
        List<Tile> finishedList = new List<Tile>();

        Tile currentTile = end;

        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    // Returns the cardinal distance from one tile to another
    private float GetManhattanDistance(Tile start, Tile neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    // Returns all of the touching tiles next to the specified tile
    private List<Tile> GetNeighbourTiles(Tile currentTile) 
    {
        var map = GridManager.Instance.tiles;

        List<Tile> neighbours = new List<Tile>();

        // Top tile
        Vector2 locationToCheck = new Vector2(currentTile.gridLocation.x, currentTile.gridLocation.y + 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        // Left tile
        locationToCheck = new Vector2(currentTile.gridLocation.x - 1, currentTile.gridLocation.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        // Right tile
        locationToCheck = new Vector2(currentTile.gridLocation.x + 1, currentTile.gridLocation.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        // Bottom tile
        locationToCheck = new Vector2(currentTile.gridLocation.x, currentTile.gridLocation.y - 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}

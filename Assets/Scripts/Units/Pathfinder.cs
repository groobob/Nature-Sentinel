using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder
{
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
                //finalizing the path
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GetNeighbourTiles(currentTile);

            foreach (var neighbour in neighbourTiles)
            {
                if(!neighbour.walkable || closedList.Contains(neighbour))
                {
                    continue;
                }

                neighbour.G = GetManhattanDistance(start, neighbour);
                neighbour.G = GetManhattanDistance(end, neighbour);

                neighbour.previous = currentTile;

                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<Tile>();
    }

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

            if (currentTile.distance == distance)
            {
                foreach (Tile tile in openList)
                {
                    openList.Add(tile);
                }
                break;
            }

            int newDistance = currentTile.distance + 1;

            foreach (Tile tile in GetNeighbourTiles(currentTile))
            {
                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                    tile.distance = newDistance;
                }
            }

            currentIndex++;
        }
        return openList;
    }

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

    private int GetManhattanDistance(Tile start, Tile neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    private List<Tile> GetNeighbourTiles(Tile currentTile) 
    {
        var map = GridManager.Instance.tiles;

        List<Tile> neighbours = new List<Tile>();

        //top tile
        Vector2Int locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y + 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //left tile
        locationToCheck = new Vector2Int(currentTile.gridLocation.x - 1, currentTile.gridLocation.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //right tile
        locationToCheck = new Vector2Int(currentTile.gridLocation.x + 1, currentTile.gridLocation.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //bottom tile
        locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y - 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}

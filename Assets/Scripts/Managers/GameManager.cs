using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPlayers:
                UnitManager.Instance.SpawnPlayers();
                break;
            case GameState.SpawnEnemies:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void HandleSelectColor()
    {
        
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnPlayers = 1,
    SpawnEnemies = 2,
    PlayerTurn = 3,
    EnemyTurn = 4,
}

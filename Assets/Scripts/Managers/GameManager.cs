using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*
 * Manages the state of the game
 * Tells game what to run/do whenever state of the game is changed
 */

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;

    // Singleton
    void Awake()
    {
        Instance = this;
    }

    // Sets the first state to "GenerateGrid"
    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    // Changes game state to the given state
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
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                foreach (BaseEnemy enemy in UnitManager.Instance.enemies)
                {
                    enemy.Move();
                }
                ChangeState(GameState.PlayerTurn);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

// Enum to handle the game state data
public enum GameState
{
    GenerateGrid = 0,
    SpawnPlayers = 1,
    SpawnEnemies = 2,
    PlayerTurn = 3,
    EnemyTurn = 4,
}

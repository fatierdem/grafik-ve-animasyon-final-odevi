using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { Menu, Game, LevelComplete, Gameover, Idle}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState gameState;
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    public void NextButton()
    {
        SetGameState(GameState.Game);
    }
    public void PlayButton()
    {
        SetGameState(GameState.Game);
    }
    public void BackButton()
    {
        SetGameState(GameState.Menu);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}

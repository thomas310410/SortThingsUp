using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action <GameState> OnGameStateChanged;


    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        UpdateGameState(GameState.MenuP);
    }


    public void UpdateGameState(GameState newState) 
    { 
        State = newState;

        switch (newState)
        {
            case GameState.MenuP:
                break;
            case GameState.Playing:
                break;
            case GameState.LevelSelection:
                break;
            case GameState.Victory:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
   
   

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameState
{
    MenuP,
    Playing,
    LevelSelection,
    Victory
}


﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        EASY,
        MEDIUM,
        HARD,
        DIFFICULT
    }
    // Start is called before the first frame update

    public static GameSettings Instance;

    private void Awake()
    {
        _Paused = false;

        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;

        }
        else
            Destroy(this);
    }
    private EGameMode _GameMode;
    private bool _continuePreviousGame = false;
    private bool _exitAfterWon = false;
    private bool _Paused = false;

    public void SetExitAfterWon(bool set)
    {
        _exitAfterWon = set;
        _continuePreviousGame = false;
    }

    public bool GetExitAfterWon()
    {
        return _exitAfterWon;
    }

    public void SetContinuePreviousGame(bool continue_game)
    {
        _continuePreviousGame = continue_game;
    }

    public bool GetContinuePreviousGame()
    {
        return _continuePreviousGame;
    }

    public void SetPaused(bool paused) { _Paused = paused; }
    public bool GetPaused() { return _Paused; }


     void Start()
    {
        _GameMode = EGameMode.NOT_SET;
        _continuePreviousGame = false;
    }
    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }
    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(EGameMode.EASY);
        else if (mode == "Medium") SetGameMode(EGameMode.MEDIUM);
        else if (mode == "Hard") SetGameMode(EGameMode.HARD);
        else if (mode == "Difficult") SetGameMode(EGameMode.DIFFICULT);
        SetGameMode(EGameMode.NOT_SET);
    }

    public string GetGameMode()
    {
        switch(_GameMode)
            {
                case EGameMode.EASY: return "Easy";
                case EGameMode.MEDIUM: return "Medium";
                case EGameMode.HARD: return "Hard";
                case EGameMode.DIFFICULT: return "Difficult";
        }
        Debug.LogError("ERROR: Game Level is not set");
        return " ";

    }
}

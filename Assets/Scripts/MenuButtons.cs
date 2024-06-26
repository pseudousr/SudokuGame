﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadEasyGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.EASY);
        SceneManager.LoadScene(name);
    }
    public void LoadMediumGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.MEDIUM);
        SceneManager.LoadScene(name);
    }
    public void LoadHardGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.HARD);
        SceneManager.LoadScene(name);
    }
    public void LoadDifficultGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.DIFFICULT);
        SceneManager.LoadScene(name);
    }
    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);

    }
    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);

    }

    public void SetPaused(bool paused)
    {
        GameSettings.Instance.SetPaused(paused);
    }

    public void ContinuePreviousGame(bool continue_game)
    {
        GameSettings.Instance.SetContinuePreviousGame(continue_game);
    }
    public void ExitAfterWon()
    {
        GameSettings.Instance.SetExitAfterWon(true);
    }
}

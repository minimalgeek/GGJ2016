using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{

    public void LoadNextLevel()
    {
        string sceneName = "Level" + (GameState.instance.LevelState.currentLevel + 1);
        GameState.instance.LevelState.currentLevel++;
        try {
            Application.LoadLevel(sceneName);
        } catch (Exception e)
        {
            GameState.instance.LevelState.currentLevel = 1;
        }
    }

    public void LoadCurrentLevel()
    {
        string sceneName = "Level" + (GameState.instance.LevelState.currentLevel);
        try
        {
            Application.LoadLevel(sceneName);
        }
        catch (Exception e)
        {
            GameState.instance.LevelState.currentLevel = 1;
        }
    }

}

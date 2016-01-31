using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadNextLevel()
    {
        string sceneName = "Level" + GameState.instance.LevelState.currentLevel + 1;
        if (SceneManager.GetSceneByPath(sceneName) != null)
        {
            GameState.instance.LevelState.currentLevel++;
            SceneManager.LoadScene(sceneName);
        } else
        {
            GameState.instance.LevelState.currentLevel = 1;
            LoadCurrentLevel();
        }
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + GameState.instance.LevelState.currentLevel);
    }

}

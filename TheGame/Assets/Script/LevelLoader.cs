using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadNextLevel()
    {
        GameState.instance.LevelState.currentLevel++;
        string sceneName = "Level" + GameState.instance.LevelState.currentLevel;
        if (SceneManager.GetSceneByName(sceneName) != null)
        {
            SceneManager.LoadScene(sceneName);
        } else
        {
            GameState.instance.LevelState.currentLevel--; // hák
        }
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + GameState.instance.LevelState.currentLevel);
    }

}

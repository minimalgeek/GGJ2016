using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadNextLevel()
    {
        GameState.instance.LevelState.currentLevel++;
        SceneManager.LoadScene("Level" + GameState.instance.LevelState.currentLevel);
    }

    public void LoadCurrentLevel()
    {
        //GameState.instance.LevelState.currentLevel++;
        SceneManager.LoadScene("Level" + GameState.instance.LevelState.currentLevel);
    }

}

using UnityEngine;
using System.Collections;

public class GameStateHolder : MonoBehaviour
{
    public GameState gameState;

    void Awake()
    {
        GameState.instance.TryLoadFromAssets(Application.persistentDataPath);
    }

    void OnDestroy()
    {
        GameState.instance.SaveToFile(Application.persistentDataPath);
    }
}
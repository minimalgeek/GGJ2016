using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameState : LoadableData
{

    #region Singleton access
    private static GameState _instance;
    public static GameState instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameStateHolder>().gameState;
            }
            return _instance;
        }
    }

    private GameState()
    {
        CultistState = new CultistState();
    }
    #endregion

    private CultistState cultistState;
    
    public CultistState CultistState
    {
        get
        {
            return cultistState;
        }

        set
        {
            cultistState = value;
        }
    }

    #region Overridden functions for loading/saving
    protected override void LoadData(MemoryStream ms)
    {
        IFormatter formatter = new BinaryFormatter();
        GameState gd = (GameState)formatter.Deserialize(ms);

        this.CultistState = gd.CultistState == null ? new CultistState() : gd.CultistState;
    }

    public override string GetFileName()
    {
        return "gamestate";
    }

    #endregion
}

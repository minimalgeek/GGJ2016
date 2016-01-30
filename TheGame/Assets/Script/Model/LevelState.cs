using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelState
{
    public int currentLevel;
    [System.NonSerialized]
    private ArrayList executedRituals;

    public LevelState()
    {
        executedRituals = new ArrayList();
    }

    public void AddExecutedRitual(string ritualName)
    {
        executedRituals.Add(ritualName);
    }
    
}

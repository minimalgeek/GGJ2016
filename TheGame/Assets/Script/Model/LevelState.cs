using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelState
{
    public int currentLevel;
    [System.NonSerialized]
    private ArrayList executedRituals;
    [System.NonSerialized]
    private int latestCompletedIndex = -1;

    public LevelState()
    {
        currentLevel = 1;
        executedRituals = new ArrayList();
    }

    public int LatestCompletedIndex
    {
        get
        {
            return latestCompletedIndex;
        }
    }

    public void AddExecutedRitual(string ritualName)
    {
        executedRituals.Add(ritualName);
        latestCompletedIndex++;
    }
    
    public void ResetExecutedRituals()
    {
        executedRituals = new ArrayList();
        latestCompletedIndex = -1;
    }
}

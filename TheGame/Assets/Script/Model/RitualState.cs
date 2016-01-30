using UnityEngine;
using System.Collections;

[System.Serializable]
public class RitualState
{
    private ArrayList executedRituals;

    public RitualState()
    {
        executedRituals = new ArrayList();
    }

    public void AddExecutedRitual(string ritualName)
    {
        executedRituals.Add(ritualName);
    }
    
}

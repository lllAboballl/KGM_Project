using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] List<StatClass> statList = new List<StatClass>();
    [SerializeField] int[] upgradeableStatsArray;

    public void IncreaseStat(string statName, float increaseValue)
    {
        foreach (StatClass stat in statList)
        {
            if (stat.name == statName)
            {
                Debug.Log("randomValue" + increaseValue);
                stat.statValue *= increaseValue;
                return;
            }
        }
    }
    
    public StatClass GetStat(string statName)
    {
        if (statName == "random")
        {
            int randomStatIndex = upgradeableStatsArray[Random.Range(0, upgradeableStatsArray.Length - 1)];
            return statList.ToArray()[randomStatIndex];
        }
        foreach (StatClass stat in statList)
        {
            if (statName == stat.name)
            {
                return stat;
            }
        }
        Debug.LogError("Failed stat request");
        return statList.ToArray()[0];
    }



}
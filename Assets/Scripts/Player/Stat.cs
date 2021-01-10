using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statName;

    public bool MaxLevel { get; private set; } = false;

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if (level >= values.Length - 1) MaxLevel = true;            
        }
    }

    [SerializeField] private float[] values;
    public float Value
    {
        get { return values[level]; }
    }
    public float NextLevelValue
    {
        get
        {
            if (!MaxLevel) return values[level + 1];
            else
            {
                Debug.LogError("Error");
                return -1;
            }
        }
    }
    public float GetValue(int level)
    {
        return values[level];
    }
    public float[] Values { get { return values; } }

    public Stat(string statName, float[] values)
    {
        this.statName = statName;        
        this.values = values;
        Level = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statName;

    [HideInInspector] public bool maxLevel;

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if (level >= this.value.Length - 1) maxLevel = true;
        }
    }

    [SerializeField] private float[] value;
    public float Value
    {
        get { return value[level]; }
    }
    public float NextLevelValue
    {
        get
        {
            if (!maxLevel) return value[level + 1];
            else
            {
                Debug.LogError("Error");
                return -1;
            }
        }
    }
    public float GetValue(int level)
    {
        return value[level];
    }
}

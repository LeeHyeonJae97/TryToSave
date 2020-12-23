using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    public string name;
    [HideInInspector] public bool maxLevel;
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if (level >= this.value.Length) maxLevel = true;
        }
    }
    [SerializeField] private float[] value;
    public float Value
    {
        get { return value[level - 1]; }
    }

    public void LevelUp() => Level += 1;
}

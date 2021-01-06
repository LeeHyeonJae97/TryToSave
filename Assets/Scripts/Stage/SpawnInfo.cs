using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnInfo
{
    public string name;
    public int percent;

    public SpawnInfo(string name, int percent)
    {
        this.name = name;
        this.percent = percent;
    }
}

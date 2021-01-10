using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelInfo
{
    public int amount;
    public int percent;

    public LevelInfo(int amount, int percent)
    {
        this.amount = amount;
        this.percent = percent;
    }
}

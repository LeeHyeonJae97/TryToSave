using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrowdControl", menuName = "ScriptableObject/CrowdControl")]
public class CrowdControl : ScriptableObject
{
    public string ccName;
    public int damage;
    public float curInterval;
    public float interval;
    public int slow;
    public float duration;
    public Color materialColor;

    public CrowdControl(int damage, float interval, int slow, float duration)
    {
        this.damage = damage;
        this.curInterval = 0;
        this.interval = interval;
        this.slow = slow;
        this.duration = duration;
    }
}

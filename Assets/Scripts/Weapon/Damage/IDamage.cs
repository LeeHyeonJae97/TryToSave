using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamage : ScriptableObject
{
    protected string bloodEffect;

    public abstract void Init(CrowdControl cc, string bloodEffect);
    public abstract void Damage(GameObject target, int damage);
    public abstract void Damage(GameObject[] targets, int damage);
}

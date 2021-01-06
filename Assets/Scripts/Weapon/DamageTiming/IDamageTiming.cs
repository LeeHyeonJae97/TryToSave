using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamageTiming : ScriptableObject
{
    protected IDamage baseDamage;

    public abstract void Init(IDamage baseDamage, string bulletName, float range);
    public abstract void Damage(GameObject target, int damage);
    public abstract void Damage(GameObject[] targets, int damage);
}

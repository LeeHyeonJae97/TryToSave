using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBulletDamage : ScriptableObject
{
    protected GameObject bullet;
    protected IDamage baseDamage;
    protected string hitEffectName;
    protected int damage;
    protected float range;
    protected float explosionRange;

    public abstract void Init(GameObject bullet, string hitEffectName, float explosionRange);
    public abstract Vector3 SetActive(Transform target, IDamage baseDamage, int damage, float range);
    public abstract void Hit(GameObject target);
    public abstract void FinishFly();
}

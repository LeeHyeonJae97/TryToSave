using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGetTarget : ScriptableObject
{
    public delegate GameObject GetClosestTarget(float range);
    protected GetClosestTarget getClosestTarget;
    protected IDamageTiming damageTiming;
    protected GameObject fireEffect;
    protected ParticleSystem[] pss;

    public void Init(IDamageTiming damageTiming, GetClosestTarget getClosestTarget, GameObject fireEffect)
    {
        if(damageTiming == null || getClosestTarget == null || fireEffect == null)
        {
            Debug.LogError("Error");
            return;
        }

        this.damageTiming = damageTiming;
        this.getClosestTarget = getClosestTarget;
        this.fireEffect = fireEffect;
        pss = fireEffect.GetComponentsInChildren<ParticleSystem>();
    }

    protected void Fire(Vector3 targetPos)
    {
        Vector3 dir = targetPos - Player.Pos;
        dir.y = 0;
        fireEffect.transform.position = Player.Pos + dir.normalized * 0.8f;
        fireEffect.transform.forward = -dir;

        for (int i = 0; i < pss.Length; i++) pss[i].Play();
    }

    public abstract bool GetTarget(int damage, float range, float hitRange);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGetTarget : ScriptableObject
{
    public delegate GameObject GetClosestTarget(float range);
    protected GetClosestTarget getClosestTarget;
    protected IDamageTiming damageTiming;
    protected GameObject fireEffect;

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
    }

    protected void Fire(Vector3 targetPos)
    {
        Vector3 dir = targetPos - Player.Pos;
        dir.y = 0;
        fireEffect.transform.position = Player.Pos + dir.normalized * 0.8f;
        fireEffect.transform.forward = -dir;
        fireEffect.GetComponent<ParticleSystem>().Play();
    }

    public abstract bool GetTarget(int damage, float range, float hitRange);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustTarget : IGetTarget
{
    public delegate GameObject GetOrgTarget(float range);
    private GetOrgTarget getOrgTarget;

    private IDamageTiming damageTiming;

    public JustTarget(GetOrgTarget getOrgTarget, IDamageTiming damageTiming)
    {
        this.getOrgTarget = getOrgTarget;
        this.damageTiming = damageTiming;
    }

    public bool Damage(int amount, float damage, float range)
    {
        GameObject target = getOrgTarget(range);
        if (target != null)
        {
            damageTiming.Damage(target, damage);
            return true;
        }
        else return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAfterShoot : IDamageTiming
{
    private IDebuff debuff;

    public RightAfterShoot(IDebuff debuff)
    {
        this.debuff = debuff;
    }

    public void Damage(GameObject target, float damage) => debuff.Damage(target, damage);

    public void Damage(GameObject[] targets, float damage) => debuff.Damage(targets, damage);
}

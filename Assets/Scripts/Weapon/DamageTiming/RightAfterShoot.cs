using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RightAfterShoot", menuName = "ScriptableObject/Weapon/RightAfterShoot")]
public class RightAfterShoot : IDamageTiming
{
    public override void Init(IDamage baseDamage, string bulletName, float range)
    {
        if(baseDamage == null)
        {
            Debug.LogError("Error");
            return;
        }

        this.baseDamage = baseDamage;
    }

    public override void Damage(GameObject target, int damage)
    {
        if (target == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        baseDamage.Damage(target, damage);
    }

    public override void Damage(GameObject[] targets, int damage)
    {
        if (targets == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        baseDamage.Damage(targets, damage);
    }
}

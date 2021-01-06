using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JustTarget", menuName = "ScriptableObject/Weapon/JustTarget")]
public class JustTarget : IGetTarget
{
    public override bool GetTarget(int damage, float range, float hitRange)
    {
        if (damage == 0 || range == 0)
        {
            Debug.LogError("Error");
            return false;
        }

        GameObject target = getClosestTarget(range);
        if (target != null)
        {
            Fire(target.transform.position);
            damageTiming.Damage(target, damage);
            return true;
        }
        else return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeTarget", menuName = "ScriptableObject/Weapon/RangeTarget")]
public class RangeTarget : IGetTarget
{
    public override bool GetTarget(int damage, float range, float hitRange)
    {
        if (damage == 0 || range == 0 || hitRange == 0)
        {
            Debug.LogError("Error");
            return false;
        }

        GameObject target = getClosestTarget(range);
        if (target != null)
        {
            Collider[] colls = Physics.OverlapSphere(target.transform.position, hitRange, 1 << LayerMask.NameToLayer("Zombie"));

            GameObject[] targets = new GameObject[colls.Length];
            for (int i = 0; i < targets.Length; i++) targets[i] = colls[i].gameObject;

            Fire(target.transform.position);
            damageTiming.Damage(targets, damage);

            return true;
        }
        else return false;
    }
}

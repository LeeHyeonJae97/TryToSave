using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearTarget : IGetTarget
{
    public delegate GameObject GetOrgTarget(float range);
    private GetOrgTarget getOrgTarget;

    private IDamageTiming damageTiming;

    public LinearTarget(GetOrgTarget getOrgTarget, IDamageTiming damageTiming)
    {
        this.getOrgTarget = getOrgTarget;
        this.damageTiming = damageTiming;
    }

    public bool Damage(int amount, float damage, float range)
    {
        GameObject target = getOrgTarget(range);
        if (target != null)
        {
            Vector3 dir = target.transform.position - Player.Pos;
            dir.y = 0;
            RaycastHit[] hits = Physics.RaycastAll(Player.Pos + Vector3.up * 0.25f, dir.normalized, range, 1 << LayerMask.NameToLayer("Zombie"));

            amount = hits.Length >= amount ? amount : hits.Length;

            GameObject[] targets = new GameObject[amount];
            for (int i = 0; i < amount; i++) targets[i] = hits[i].collider.gameObject;

            damageTiming.Damage(targets, damage);

            return true;
        }
        else return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTarget : IGetTarget
{
    public delegate GameObject GetOrgTarget(float range);
    private GetOrgTarget getOrgTarget;

    private IDamageTiming damageTiming;

    public RangeTarget(GetOrgTarget getOrgTarget, IDamageTiming damageTiming)
    {
        this.getOrgTarget = getOrgTarget;
        this.damageTiming = damageTiming;
    }

    //**
    // range값 어떻게 결정할지??
    //**

    public bool Damage(int amount, float damage, float range)
    {
        GameObject target = getOrgTarget(range);
        if (target != null)
        {
            Collider[] colls = Physics.OverlapSphere(target.transform.position, range * 2, 1 << LayerMask.NameToLayer("Zombie"));

            amount = colls.Length >= amount ? amount : colls.Length;
            Debug.Log(colls.Length);

            GameObject[] targets = new GameObject[amount];
            for (int i = 0; i < amount; i++) targets[i] = colls[i].gameObject;

            damageTiming.Damage(targets, damage);

            return true;
        }
        else return false;
    }
}

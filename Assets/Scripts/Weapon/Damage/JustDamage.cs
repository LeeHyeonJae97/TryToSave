using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDamage : IDebuff
{
    public void Damage(GameObject target, float damage)
    {
        target.GetComponent<Zombie>().Attacked((int)damage);
    }

    public void Damage(GameObject[] targets, float damage)
    {
        for (int i = 0; i < targets.Length; i++)
            targets[i].GetComponentInParent<Zombie>().Attacked((int)damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JustDamage", menuName = "ScriptableObject/Weapon/JustDamage")]
public class JustDamage : IDamage
{
    public override void Init(CrowdControl cc, string bloodEffect)
    {
        if(bloodEffect.CompareTo("") == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.bloodEffect = bloodEffect;        
    }

    public override void Damage(GameObject target, int damage)
    {
        if (target == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        target.GetComponent<Zombie>().Attacked(damage, bloodEffect);
    }

    public override void Damage(GameObject[] targets, int damage)
    {
        if (targets == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        for (int i = 0; i < targets.Length; i++)
            targets[i].GetComponent<Zombie>().Attacked(damage, bloodEffect);
    }
}

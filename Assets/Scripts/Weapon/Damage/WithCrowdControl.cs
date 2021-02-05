using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WithDebuff", menuName = "ScriptableObject/Weapon/WithDebuff")]
public class WithCrowdControl : IDamage
{
    private CrowdControl cc;

    public override void Init(CrowdControl cc, string bloodEffect)
    {
        if (cc == null || bloodEffect.CompareTo("") == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.cc = cc;
        this.bloodEffect = bloodEffect;
    }

    public override void Damage(GameObject target, int damage)
    {
        if (target == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        Zombie zombie = target.GetComponent<Zombie>();
        zombie.Attacked(damage, bloodEffect);

        // 풀링한다면 최적화 가능할듯
        //zombie.SetCrowdControl(Instantiate(cc));
    }

    public override void Damage(GameObject[] targets, int damage)
    {
        if (targets == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            Zombie zombie = targets[i].GetComponent<Zombie>();
            zombie.Attacked(damage, bloodEffect);

            // 풀링한다면 최적화 가능할듯
            //zombie.SetCrowdControl(Instantiate(cc));
        }
    }
}

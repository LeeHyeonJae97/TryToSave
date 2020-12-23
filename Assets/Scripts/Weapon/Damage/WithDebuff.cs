using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithDebuff : IDebuff
{
    private string debuffType;

    public WithDebuff(string type)
    {
        debuffType = type;
    }

    public void Damage(GameObject target, float damage)
    {
        Zombie zombie = target.GetComponent<Zombie>();
        zombie.Attacked((int)damage);
        //zombie.SetState(debuffType);
    }

    public void Damage(GameObject[] targets, float damage)
    {
        for(int i=0; i<targets.Length; i++)
        {
            Zombie zombie = targets[i].GetComponent<Zombie>();
            zombie.Attacked((int)damage);
            //zombie.SetState(debuffType);
        }
    }
}

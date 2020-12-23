using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : IDamageTiming
{
    public string bulletName;

    private IDebuff debuff;

    public HitBullet(string bulletName, IDebuff debuff)
    {
        this.bulletName = bulletName;
        this.debuff = debuff;
    }

    // 총알이 한개라서 한 마리만 맞는 경우
    public void Damage(GameObject target, float damage)
    {
        GameObject bullet = PoolingManager.instance.Get(bulletName);

        //bullet의 IDamage.Damage가 바뀔 일이 없으려나???
        //bullet.GetComponent<Bullet>().Init(target, damage, debuff.Damage);
    }

    // 총알이 여러개라 여러 마리가 맞는 경우
    public void Damage(GameObject[] targets, float damage)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            GameObject bullet = PoolingManager.instance.Get(bulletName);

            //bullet의 IDamage.Damage가 바뀔 일이 없으려나???
            //bullet.GetComponent<Bullet>().Init(target, damage, debuff.Damage);
        }
    }
}

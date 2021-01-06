using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitBullet", menuName = "ScriptableObject/Weapon/HitBullet")]
public class HitBullet : IDamageTiming
{
    public float bulletFirePosY;

    private string bulletName;
    private float range;

    public override void Init(IDamage baseDamage, string bulletName, float range)
    {
        if (baseDamage == null || bulletName.CompareTo("") == 0 || range == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.baseDamage = baseDamage;
        this.bulletName = bulletName;
        this.range = range;
    }

    // 한 마리에게 한 개 총알이 발사되는 경우
    public override void Damage(GameObject target, int damage)
    {
        if(target == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        GameObject bullet = PoolingManager.instance.Get(bulletName, Player.Pos + Vector3.up * bulletFirePosY);
        bullet.GetComponent<Bullet>().SetActive(target, baseDamage, damage, range);
    }

    // 여러 마리에게 여러 총알이 발사되는 경우
    public override void Damage(GameObject[] targets, int damage)
    {
        if (targets == null || damage == 0)
        {
            Debug.LogError("Error");
            return;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            if(targets[i] == null)
            {
                Debug.LogError("Error");
                return;
            }

            GameObject bullet = PoolingManager.instance.Get(bulletName, Player.Pos + Vector3.up * bulletFirePosY);
            bullet.GetComponent<Bullet>().SetActive(targets[i], baseDamage, damage, range);
        }
    }
}

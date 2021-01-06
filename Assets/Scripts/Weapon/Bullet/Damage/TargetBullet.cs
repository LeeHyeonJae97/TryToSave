using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target", menuName = "ScriptableObject/Bullet/Target")]
public class TargetBullet : IBulletDamage
{
    public override void Init(GameObject bullet, string hitEffectName, float explosionRange)
    {
        if (bullet == null || hitEffectName.CompareTo("") == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.bullet = bullet;
        this.hitEffectName = hitEffectName;
    }

    public override Vector3 SetActive(Transform target, IDamage baseDamage, int damage, float range)
    {
        if (target == null || baseDamage == null || damage == 0 || range == 0)
        {
            Debug.LogError("Error");
            return Vector3.zero;
        }

        this.baseDamage = baseDamage;
        this.damage = damage;
        this.range = range;

        Vector3 dir = target.position - bullet.transform.position;
        dir.y = 0;
        return bullet.transform.position + dir.normalized * range;
    }

    public override void Hit(GameObject target)
    {
        baseDamage.Damage(target, damage);

        PoolingManager.instance.Get(hitEffectName, bullet.transform.position);

        PoolingManager.instance.Return(bullet);
    }

    public override void FinishFly()
    {
        PoolingManager.instance.Return(bullet);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosive", menuName = "ScriptableObject/Bullet/Explosive")]
public class ExplosiveBullet : IBulletDamage
{
    public override void Init(GameObject bullet, string hitEffectName ,float explosionRange)
    {
        if (bullet == null || hitEffectName.CompareTo("") == 0 || explosionRange == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.bullet = bullet;
        this.hitEffectName = hitEffectName;
        this.explosionRange = explosionRange;
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

        return target.position;
    }

    public override void Hit(GameObject target) { }

    public override void FinishFly()
    {
        Collider[] colls = Physics.OverlapSphere(bullet.transform.position, explosionRange, 1 << LayerMask.NameToLayer("Zombie"));

        GameObject[] targets = new GameObject[colls.Length];
        for (int i = 0; i < targets.Length; i++) targets[i] = colls[i].gameObject;

        baseDamage.Damage(targets, damage);

        PoolingManager.instance.Get(hitEffectName, bullet.transform.position);

        PoolingManager.instance.Return(bullet);        
    }
}

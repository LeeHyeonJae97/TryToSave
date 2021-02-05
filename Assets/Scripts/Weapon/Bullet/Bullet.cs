using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string bulletName;
    public string hitEffectName;
    public float speed;
    public float explosionRange;

    [Tooltip("Arc / Linear")]
    public IBulletMove baseMove;
    [Tooltip("Target / Splash / Explosive")]
    public IBulletDamage baseDamage;

    private void Awake()
    {
        baseMove = Instantiate(baseMove);
        baseDamage = Instantiate(baseDamage);

        baseDamage.Init(gameObject, hitEffectName, explosionRange);
        baseMove.Init(transform, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie")) baseDamage.Hit(other.gameObject);
    }

    private void Update()
    {
        if (baseMove.Move()) baseDamage.FinishFly();
    }

    public void SetActive(GameObject target, IDamage baseDamage, int damage, float range)
    {
        Vector3 targetPos = this.baseDamage.SetActive(target.transform, baseDamage, damage, range);
        baseMove.SetActive(targetPos);
    }
}

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
    public IBulletMove move;
    [Tooltip("Target / Splash / Explosive")]
    public IBulletDamage bulletDamage;

    private void Awake()
    {
        move = Instantiate(move);
        bulletDamage = Instantiate(bulletDamage);

        bulletDamage.Init(gameObject, hitEffectName, explosionRange);
        move.Init(transform, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie")) bulletDamage.Hit(other.gameObject);
    }

    private void Update()
    {
        if (move.Move()) bulletDamage.FinishFly();
    }

    public void SetActive(GameObject target, IDamage baseDamage, int damage, float range)
    {
        Vector3 targetPos = bulletDamage.SetActive(target.transform, baseDamage, damage, range);
        move.SetActive(targetPos);
    }
}

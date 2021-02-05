using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public delegate void DieCallback(GameObject go, int point);
    private DieCallback onDie;

    public Animator anim;
    public Rigidbody rb;
    public Collider coll;
    public Transform model;

    public SkinnedMeshRenderer mesh;
    private MaterialPropertyBlock propBlock;
    private Color orgMatColor;

    public int minMaxHp;
    public int maxMaxHp;
    private int curHp;
    public int damage;
    private int curDamage;
    public float cooldown = 1;
    private float curCooldown;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    private float curMoveSpeed;
    public int exp;
    private bool dead;

    private void OnCollisionStay(Collision collision)
    {
        if (!dead)
        {
            GameObject go = collision.gameObject;
            if (go.CompareTag("Player") && curCooldown >= cooldown)
            {
                curCooldown = 0;
                go.GetComponent<Player>().CurHp -= curDamage;
            }
        }
    }

    private void Update()
    {
        // 공격 쿨타임 계산
        if (!dead) curCooldown += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Vector3 dir = (Player.Pos - rb.position).normalized;

            if (dir != Vector3.zero)
            {
                model.forward = dir;
                rb.MovePosition(rb.position + dir * Time.deltaTime * curMoveSpeed);
            }
        }
    }

    public void Init(int level, DieCallback onDie)
    {
        this.onDie = onDie;

        coll.enabled = true;
        dead = false;

        // 공격력, 체력과 이동속도는 현재 플레이어의 레벨에 비례
        float proportion = 1 + level * 0.05f;
        curDamage = (int)(damage * proportion);
        curHp = (int)(Random.Range(minMaxHp, maxMaxHp + 1) * proportion);
        curMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed + 1) * proportion;

        // 이동 속도에 맞춰 애니메이션 재생 속도 조절
        anim.speed = curMoveSpeed / 5;
        anim.Play("run");

        InvokeRepeating(nameof(CheckVelocity), 1, 0.5f);
    }

    public void Attacked(int damage, string bloodEffect)
    {
        curHp -= damage;
        PoolingManager.instance.Get(bloodEffect, transform.position + Vector3.up * 0.01f, model.rotation);

        if (curHp <= 0)
        {
            coll.enabled = false;
            dead = true;

            anim.speed = 1;
            anim.Play("die");

            // 경험치 획득, 공격 가능한 좀비 리스트에서 제거는 바로 이루어지지만
            // 오브젝트를 다시 풀에 반납하는 일은 3초 뒤에 처리
            onDie(gameObject, exp);
            Invoke(nameof(Return), 4);
        }
    }

    private void Return()
    {
        PoolingManager.instance.Return(gameObject);
    }

    // 충돌로 인해 속도가 생기지 않도록 주기적으로 체크
    private void CheckVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public delegate void Die(GameObject go, int point);
    private Die die;

    public Animator anim;
    public Rigidbody rb;
    public Transform model;

    public string key;
    private int curHp;
    public int maxHp;
    public int damage;
    public float moveSpeed;
    public int point;

    public void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Player")) go.GetComponent<Player>().CurHp -= damage;
    }

    private void FixedUpdate()
    {        
        Vector3 dir = (Player.Pos - rb.position).normalized;

        if(dir != Vector3.zero)
        {
            // 바라보는 방향 결정
            model.forward = dir;

            // 이동
            rb.MovePosition(rb.position + dir * Time.deltaTime * moveSpeed);
        }     
    }

    public void Init(Die die)
    {
        this.die = die;
        curHp = maxHp;

        InvokeRepeating(nameof(CheckVelocity), 1, 0.5f);
    }

    public void Attacked(int damage)
    {
        curHp -= damage;
        if (curHp <= 0) die(gameObject, point);        
    }

    private void CheckVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}

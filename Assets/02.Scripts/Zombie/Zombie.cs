using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public delegate void Die(GameObject go, int point);
    private Die die;

    public Transform player;

    public Animator anim;
    public Rigidbody rb;
    public Transform model;

    public string key;
    public int hp;
    public int damage;
    public float moveSpeed;
    public int point;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) Attack();
    }

    private void FixedUpdate()
    {        
        Vector3 dir = (player.position - rb.position).normalized;

        if(dir != Vector3.zero)
        {
            // 바라보는 방향 결정
            model.forward = dir;

            // 이동
            rb.MovePosition(rb.position + dir * Time.deltaTime * moveSpeed);
        }     
    }

    public void Init(Transform player, Die die)
    {
        this.player = player;
        this.die = die;   

        InvokeRepeating(nameof(CheckVelocity), 1, 0.5f);
    }

    public void Attack()
    {
        player.GetComponent<Player>().CurHp -= damage;
    }

    public void Attacked(int damage)
    {
        hp -= damage;
        if (hp <= 0) die(gameObject, point);
    }

    private void CheckVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}

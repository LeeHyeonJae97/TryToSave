using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Vector3 Pos;

    public Transform model;
    public Rigidbody rb;

    public Image healthBar;

    public int maxHp;
    public int damage;
    public float moveSpeed;
    public float size; // 기본에서 몇 배인지
    public int fuel;
    public int fuelEfficiency;

    private int curHp;
    public int CurHp
    {
        get { return curHp; }

        set
        {
            curHp = value;

            float ratio = (float)curHp / maxHp;
            healthBar.fillAmount = ratio;

            if (ratio <= 0) Debug.Log("GAME OVER");
            else if (ratio < 0.2f) healthBar.color = new Color32(238, 59, 53, 255);
            else if (ratio < 0.4f) healthBar.color = new Color32(236, 239, 53, 255);
        }
    }

    public List<Weapon> weapons = new List<Weapon>();

    private void Start()
    {
        CurHp = maxHp;

        InvokeRepeating(nameof(CheckVelocity), 1, 0.5f);
    }

    private void Update()
    {
        Pos = model.position;
    }

    public void Move(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            // 바라보는 방향 결정
            model.forward = dir;

            // 이동
            rb.MovePosition(rb.position + dir * Time.deltaTime * moveSpeed);
        }
    }

    private void CheckVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}

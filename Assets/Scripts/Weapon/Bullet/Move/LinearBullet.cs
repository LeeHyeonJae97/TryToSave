using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Linear", menuName = "ScriptableObject/Bullet/Linear")]
public class LinearBullet : IBulletMove
{
    private Vector3 targetPos;

    public override void SetActive(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public override bool Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if ((transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            transform.position = targetPos;
            return true;
        }
        else return false;
    }
}

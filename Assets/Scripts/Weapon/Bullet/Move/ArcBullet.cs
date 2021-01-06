using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arc", menuName = "ScriptableObject/Bullet/Arc")]
public class ArcBullet : IBulletMove
{
    private const float g = 9.8f;

    private Vector3 startPos;
    private float maxHeight;
    private float duration;
    private float elapsedTime;
    private float tx, ty, tz;

    public override void SetActive(Vector3 targetPos)
    {
        startPos = transform.position;

        maxHeight = Vector3.Distance(targetPos, startPos) / 1.5f;

        ty = Mathf.Sqrt(2 * g * maxHeight);
        duration = 2 * ty / g;
        tx = (targetPos.x - startPos.x) / duration;
        tz = (targetPos.z - startPos.z) / duration;

        elapsedTime = 0;
    }

    public override bool Move()
    {
        elapsedTime += Time.deltaTime * speed;

        var tx = startPos.x + this.tx * elapsedTime;
        var ty = startPos.y + this.ty * elapsedTime - 0.5f * g * elapsedTime * elapsedTime;
        var tz = startPos.z + this.tz * elapsedTime;
        var targetPos = new Vector3(tx, ty, tz);

        transform.LookAt(targetPos);
        transform.position = targetPos;

        if (elapsedTime >= duration)
        {
            transform.position = targetPos;
            return true;
        }
        else return false;
    }
}

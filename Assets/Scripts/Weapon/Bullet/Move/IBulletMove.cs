using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBulletMove : ScriptableObject
{
    protected Transform transform;
    protected float speed;

    public void Init(Transform transform, float speed)
    {
        if (transform == null || speed == 0)
        {
            Debug.LogError("Error");
            return;
        }

        this.transform = transform;
        this.speed = speed;
    }

    public abstract void SetActive(Vector3 targetPos);
    public abstract bool Move();
}

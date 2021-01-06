using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    public delegate void Remove(GameObject go);

    protected Remove remove;

    protected int level;

    public void Init(Remove remove, Vector3 pos, int level)
    {
        if (remove == null)
        {
            Debug.LogError("Error");
            return;
        }

        this.remove = remove;
        transform.position = pos;
        this.level = level;
    }
}

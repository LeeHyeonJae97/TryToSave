using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    public delegate void Remove(GameObject go);

    protected Remove remove;
    protected int level;

    public abstract void Init(Remove remove, Vector3 pos);
}

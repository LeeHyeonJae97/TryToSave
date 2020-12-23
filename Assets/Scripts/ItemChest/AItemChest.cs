using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AItemChest : MonoBehaviour
{
    public delegate void Remove(GameObject go);
    protected Remove remove;

    public string key;
    public Transform cover;

    protected void Return()
    {
        cover.rotation = Quaternion.Euler(Vector3.zero);
        remove(gameObject);
    }

    public abstract void Init(GameManager gameManager, Remove remove, Vector3 pos, int level);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarrel : MonoBehaviour
{
    public delegate void Remove(GameObject go);
    private Remove remove;
    private float amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.CurFuel += amount;
            player.Gain("Fuel up!");
            remove(gameObject);
        }

        // 다른 오브젝트와 자리가 겹치면 그냥 바로 제거
        else if (other.CompareTag("Stuff") || other.CompareTag("Crate") || other.CompareTag("FuelBarrel"))
            remove(gameObject);
    }

    public void Init(Remove remove, Vector3 pos, float amount)
    {
        this.remove = remove;
        transform.position = pos;
        this.amount = amount;
    }
}

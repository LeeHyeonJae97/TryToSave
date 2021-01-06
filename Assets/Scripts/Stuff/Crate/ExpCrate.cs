using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrate : Crate
{
    private static int[] amounts = { 20, 30, 50, 80, 100 };    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Exp += amounts[level];
            player.Gain("Exp up!");

            remove(gameObject);
        }

        // 다른 오브젝트와 자리가 겹치면 그냥 제거해 버린다.
        else if (other.CompareTag("Stuff") || other.CompareTag("Crate") || other.CompareTag("FuelBarrel"))
            remove(gameObject);
    }
}

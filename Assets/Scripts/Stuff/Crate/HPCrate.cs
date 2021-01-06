using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCrate : Crate
{
    private static int[] amounts = { 10, 20, 40, 80, 100 };
    private static int[] levelPercents = { 50, 30, 10, 7, 3 };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.CurHp += amounts[level];
            player.Gain("Hp up!");

            remove(gameObject);
        }

        // 다른 오브젝트와 자리가 겹치면 그냥 제거해 버린다.
        else if (other.CompareTag("Stuff") || other.CompareTag("Crate") || other.CompareTag("FuelBarrel"))        
            remove(gameObject);        
    }
}

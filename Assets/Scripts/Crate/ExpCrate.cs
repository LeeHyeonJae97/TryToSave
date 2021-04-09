using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrate : Crate
{
    private static LevelInfo[] levelInfos =
    {
        new LevelInfo(5, 60),
        new LevelInfo(7, 20),
        new LevelInfo(12, 10),
        new LevelInfo(15, 8),
        new LevelInfo(30, 2)
    };
    private int amount;

    public override void Init(Remove remove, Vector3 pos)
    {
        if (remove == null)
        {
            Debug.LogError("Error");
            return;
        }

        this.remove = remove;
        transform.position = pos;

        amount = levelInfos[MyRandom.Random(levelInfos)].amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Exp += amount;
            player.Gain("Exp up!");

            remove(gameObject);
        }

        // 다른 오브젝트와 자리가 겹치면 그냥 제거해 버린다.
        else if (other.CompareTag("Stuff") || other.CompareTag("Crate") || other.CompareTag("FuelBarrel"))
            remove(gameObject);
    }
}

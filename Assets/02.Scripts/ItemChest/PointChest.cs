using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChest : AItemChest
{
    private GameManager gameManager;
    private static int[] amounts = { 20, 30, 50, 80, 100 };
    private int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.Point += amount;
            cover.rotation = Quaternion.Euler(new Vector3(70, 0, 0));
            Invoke(nameof(Return), 1);
        }
    }

    public override void Init(GameManager gameManager, Remove remove, Vector3 pos, int level)
    {
        this.gameManager = gameManager;
        this.remove = remove;
        transform.position = pos;
        amount = amounts[level];
    }
}

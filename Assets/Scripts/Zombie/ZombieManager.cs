using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public int maxAmount;
    public int maxRemoveRange;

    public Player player;

    public Transform[] spawnPoses;
    private Transform holder;
    private List<GameObject> zombies = new List<GameObject>();

    private SpawnInfo[] spawnInfos;
    private string[] keys;

    private void Awake()
    {
        holder = new GameObject("ZombieHolder").transform;
    }

    public void Init(SpawnInfo[] spawnInfos)
    {
        this.spawnInfos = spawnInfos;
        InvokeRepeating(nameof(Spawn), 1, 3);
        InvokeRepeating(nameof(CheckPos), 1, 5);
    }

    /*
    public void Init(string[] keys)
    {
        this.keys = keys;
        InvokeRepeating(nameof(Spawn), 1, 3);
        InvokeRepeating(nameof(CheckPos), 1, 5);
    }
    */

    public void Spawn()
    {
        if (zombies.Count > maxAmount) return;

        int amount = Random.Range(3, 7);

        // Spawn 위치 결정
        List<int> indexes = new List<int>();
        while (indexes.Count < amount)
        {
            int index = Random.Range(0, spawnPoses.Length);
            if (!indexes.Contains(index)) indexes.Add(index);
        }

        // Spawn
        for (int i = 0; i < amount; i++)
        {
            int index = MyRandom.Random(spawnInfos);
            if (index > -1)
            {
                GameObject zombie = PoolingManager.instance.Get(spawnInfos[index].name, holder, spawnPoses[indexes[i]].position);
                zombie.GetComponent<Zombie>().Init(Remove);
                zombies.Add(zombie);
            }
            else Debug.LogError("Error");
        }
    }

    /*
    public void Spawn()
    {
        if (zombies.Count > maxAmount) return;

        int amount = Random.Range(3, 7);

        // Spawn 위치 결정
        List<int> indexes = new List<int>();
        while (indexes.Count < amount)
        {
            int index = Random.Range(0, spawnPoses.Length);
            if (!indexes.Contains(index)) indexes.Add(index);
        }

        // Spawn
        for (int i = 0; i < amount; i++)
        {
            string key = keys[Random.Range(0, keys.Length)];
            GameObject zombie = PoolingManager.instance.Get(key, holder, spawnPoses[indexes[i]].position);
            zombie.GetComponent<Zombie>().Init(Remove);
            zombies.Add(zombie);
        }
    }
    */

    public void Remove(GameObject zombie, int exp)
    {
        player.Exp += exp;
        zombies.Remove(zombie);
    }

    public void RemoveAll()
    {
        for (int i = 0; i < zombies.Count; i++)
            PoolingManager.instance.Return(zombies[i]);

        zombies.Clear();
    }

    public GameObject GetTarget(float range)
    {
        if (zombies.Count == 0) return null;

        GameObject zombie = null;
        float minDist = range * range;
        for (int i = 0; i < zombies.Count; i++)
        {
            float dist = (zombies[i].transform.position - Player.Pos).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                zombie = zombies[i];
            }
        }

        return zombie;
    }

    private void CheckPos()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            if (zombies[i].activeInHierarchy && (zombies[i].transform.position - Player.Pos).sqrMagnitude > maxRemoveRange * maxRemoveRange)
                PoolingManager.instance.Return(zombies[i]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        MyGizmos.DrawCircle(Player.Pos, Color.red, maxRemoveRange);
    }
}

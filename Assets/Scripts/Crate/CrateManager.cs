using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour
{
    public int maxAmount;
    public int maxActiveRange;
    public int maxRemoveRange;
    public int maxSpawnRange;
    public float spawnInterval;    
    public SpawnInfo[] spawnInfos;

    private Transform holder;
    private List<GameObject> crates = new List<GameObject>();

    private void Awake()
    {
        holder = new GameObject("CrateHolder").transform;
    }

    private void Update()
    {
        for (int i = 0; i < crates.Count; i++)
        {
            float dist = (Player.Pos - crates[i].transform.position).sqrMagnitude;

            // 일정 거리 이상 벗어나면 풀에 반환
            if (dist >= maxRemoveRange * maxRemoveRange)
                Remove(crates[i]);

            // 일정 거리 이상 벗어나면 비활성화
            else if (crates[i].activeInHierarchy && dist >= maxActiveRange * maxActiveRange)
                crates[i].SetActive(false);
            // 일정 거리 내로 들어오면 활성화
            else if (!crates[i].activeInHierarchy && dist < maxActiveRange * maxActiveRange)
                crates[i].SetActive(true);
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(Spawn), 0, spawnInterval);
    }

    public void Reset()
    {
        for (int i = 0; i < crates.Count; i++)
            PoolingManager.instance.Return(crates[i]);

        crates.Clear();

        CancelInvoke();
    }

    private void Spawn()
    {
        if (crates.Count > maxAmount) return;

        // 위치
        float range = Random.Range((float)maxActiveRange, maxSpawnRange);
        float rad = Random.Range(0, 361) * Mathf.Deg2Rad;
        Vector3 spawnPos = Player.Pos + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * range;

        // 소환
        GameObject crate = PoolingManager.instance.Get(spawnInfos[MyRandom.Random(spawnInfos)].name);
        crate.transform.SetParent(holder);
        crate.GetComponent<Crate>().Init(Remove, spawnPos);

        crates.Add(crate);
    }

    // Crate에서 호출된다.
    private void Remove(GameObject itemChest)
    {
        crates.Remove(itemChest);
        PoolingManager.instance.Return(itemChest);
    }

    private void OnDrawGizmosSelected()
    {
        MyGizmos.DrawCircle(Player.Pos, Color.blue, maxActiveRange);
        MyGizmos.DrawCircle(Player.Pos, Color.green, maxSpawnRange);
        MyGizmos.DrawCircle(Player.Pos, Color.red, maxRemoveRange);
    }
}

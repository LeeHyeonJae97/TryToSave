using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarrelManager : MonoBehaviour
{
    public int maxAmount;
    public int maxActiveRange;
    public int maxRemoveRange;
    public int maxSpawnRange;
    public float spawnInterval;
    public LevelInfo[] levelInfos;

    private Transform holder;
    private List<GameObject> fuelBarrels = new List<GameObject>();

    private void Awake()
    {
        holder = new GameObject("FuelBarrelHolder").transform;
    }

    private void Update()
    {
        for (int i = 0; i < fuelBarrels.Count; i++)
        {
            float dist = (Player.Pos - fuelBarrels[i].transform.position).sqrMagnitude;

            // 일정 거리 이상 벗어나면 풀에 반환
            if (dist >= maxRemoveRange * maxRemoveRange)
                Remove(fuelBarrels[i]);

            // 일정 거리 이상 벗어나면 비활성화
            else if (fuelBarrels[i].activeInHierarchy && dist >= maxActiveRange * maxActiveRange)
                fuelBarrels[i].SetActive(false);
            // 일정 거리 내로 들어오면 활성화
            else if (!fuelBarrels[i].activeInHierarchy && dist < maxActiveRange * maxActiveRange)
                fuelBarrels[i].SetActive(true);
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(Spawn), 0, spawnInterval);
    }

    public void Reset()
    {
        for (int i = 0; i < fuelBarrels.Count; i++)
            PoolingManager.instance.Return(fuelBarrels[i]);

        fuelBarrels.Clear();

        CancelInvoke();
    }

    private void Spawn()
    {
        if (fuelBarrels.Count > maxAmount) return;

        float range = Random.Range((float)maxActiveRange, maxSpawnRange);
        float rad = Random.Range(0, 361) * Mathf.Deg2Rad;
        Vector3 spawnPos = Player.Pos + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * range;

        GameObject fuelBarrel = PoolingManager.instance.Get("FuelBarrel");
        fuelBarrel.transform.SetParent(holder);
        fuelBarrel.GetComponent<FuelBarrel>().Init(Remove, spawnPos, levelInfos[MyRandom.Random(levelInfos)].amount);

        fuelBarrels.Add(fuelBarrel);
    }

    // FuelBarrel에서 호출된다.
    private void Remove(GameObject fuelBarrel)
    {
        fuelBarrels.Remove(fuelBarrel);
        PoolingManager.instance.Return(fuelBarrel);
    }

    private void OnDrawGizmosSelected()
    {
        MyGizmos.DrawCircle(Player.Pos, Color.blue, maxActiveRange);
        MyGizmos.DrawCircle(Player.Pos, Color.green, maxSpawnRange);
        MyGizmos.DrawCircle(Player.Pos, Color.red, maxRemoveRange);
    }
}

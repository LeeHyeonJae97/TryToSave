using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestManager : MonoBehaviour
{
    public GameManager gameManager;

    public Transform player;

    public Transform[] spawnPoses;
    private string[] keys = { "GoldChest", "HPChest", "PointChest" };

    private Transform holder;
    private List<GameObject> itemChests = new List<GameObject>();

    private const int maxLevel = 5;
    private const int maxAmount = 7;
    private const int maxActiveRange = 14;
    private const int maxRemoveRange = 25;

    private void Awake()
    {
        holder = new GameObject("ItemChestHolder").transform;
    }

    private void Update()
    {
        for (int i = 0; i < itemChests.Count; i++)
        {
            // 일정 거리 이상 벗어나면 풀에 반환
            if ((player.position - itemChests[i].transform.position).sqrMagnitude >= maxRemoveRange * maxRemoveRange)
                Remove(itemChests[i]);

            // 일정 거리 이상 벗어나면 비활성화
            else if (itemChests[i].activeInHierarchy && (player.position - itemChests[i].transform.position).sqrMagnitude >= maxActiveRange * maxActiveRange)
                itemChests[i].SetActive(false);
            // 일정 거리 내로 들어오면 활성화
            else if (!itemChests[i].activeInHierarchy && (player.position - itemChests[i].transform.position).sqrMagnitude < maxActiveRange * maxActiveRange)
                itemChests[i].SetActive(true);
        }
    }

    public void Init()
    {
        InvokeRepeating(nameof(Spawn), 0, 5);
    }

    private void Spawn()
    {
        if (itemChests.Count >= maxAmount) return;

        int keyIndex = Random.Range(0, keys.Length);
        int posIndex = Random.Range(0, spawnPoses.Length);
        int level = Random.Range(0, maxLevel);

        GameObject itemChest = PoolingManager.instance.Get(keys[keyIndex]);
        itemChest.transform.SetParent(holder);
        itemChest.GetComponent<AItemChest>().Init(gameManager, Remove, spawnPoses[posIndex].position, level);

        itemChests.Add(itemChest);
    }

    private void Remove(GameObject itemChest)
    {
        itemChests.Remove(itemChest);
        PoolingManager.instance.Return(itemChest);
    }
}

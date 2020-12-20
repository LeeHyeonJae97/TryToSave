using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public ZombieManager zombieManager;
    public ItemChestManager itemChestManager;
    public StuffManager stuffManager;

    public Transform player;

    public Sector sector = new Sector();
    private Vector2Int curSector;

    public StageTable stageTable;
    //private string curStage;

    private void Start()
    {
        Init("Graveyard");        
    }

    private void Update()
    {
        int x = (int)player.position.x / Sector.length;
        int y = (int)player.position.z / Sector.length;

        if (Mathf.Abs(curSector.x - x) >= Sector.halfCount || Mathf.Abs(curSector.y - y) >= Sector.halfCount)
        {
            Vector2Int newSector = new Vector2Int(x, y);
            sector.Rearrange(curSector, newSector);
            curSector = newSector;
        }
    }

    public void Init(string key)
    {        
        var stage = stageTable.Get(key);

        // PoolingManager에 Pool 추가
        for (int i = 0; i < stage.zombies.Length; i++)
        {
            var zombie = stage.zombies[i];
            PoolingManager.instance.CreatePool(zombie.key, 5, zombie.prefab);
        }
        for (int i = 0; i < stage.stuffs.Length; i++)
        {
            var stuff = stage.stuffs[i];
            PoolingManager.instance.CreatePool(stuff.key, 5, stuff.prefab);
        }

        // Zombie 생성
        zombieManager.Init(stage.GetZombieKeys());

        // ItemChest 생성
        itemChestManager.Init();

        // Stuff 생성
        Stuff[][] stuffs = stuffManager.Init(stageTable, key, stage.stuffs.Length);

        // Sector 생성
        sector.SpawnSector(Vector2Int.zero, stuffs);

        //curStage = key;
    }
}

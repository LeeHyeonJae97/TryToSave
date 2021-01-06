using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public ZombieManager zombieManager;
    public StuffManager stuffManager;
    public CrateManager crateManager;
    public FuelBarrelManager fuelBarrelManager;
    public StageTable stageTable;

    public Sector sector = new Sector();
    private Vector2Int curSector;

    private void Start()
    {
        Init("Graveyard");        
    }

    private void Update()
    {
        int x = (int)Player.Pos.x / Sector.length;
        int y = (int)Player.Pos.z / Sector.length;

        // Sector범위를 벗어났는지 체크한 뒤 벗어났다면 재정렬
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
            PoolingManager.instance.CreatePool(zombie.name, 5, zombie.prefab);
        }
        for (int i = 0; i < stage.stuffs.Length; i++)
        {
            var stuff = stage.stuffs[i];
            PoolingManager.instance.CreatePool(stuff.name, 5, stuff.prefab);
        }

        // Zombie
        zombieManager.Init(stage.GetSpawnInfos("Zombie"));
        //zombieManager.Init(stage.GetZombieNames());

        // Crate
        crateManager.Init();

        // FuelBarrel
        fuelBarrelManager.Init();

        // Stuff
        Stuff[][] stuffs = stuffManager.Init(stage.GetSpawnInfos("Stuff"));
        //Stuff[][] stuffs = stuffManager.Init(stage.GetStuffNames());

        // Sector 생성
        sector.SpawnSector(Vector2Int.zero, stuffs);
    }
}

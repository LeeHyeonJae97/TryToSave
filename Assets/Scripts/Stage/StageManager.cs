using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public ZombieManager zombieManager;
    public StuffManager stuffManager;
    public CrateManager crateManager;
    public FuelBarrelManager fuelBarrelManager;

    public Sector sector = new Sector();
    private Vector2Int curSector;

    private GameObject selectStageManager;

    private void Awake()
    {
        selectStageManager = DontDestroyObjects.instance.selectStageManager;
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

    public void Init()
    {
        Stage stage = selectStageManager.GetComponent<SelectStageManager>().SelectedStage;

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

        // Zombie 생성
        zombieManager.Init(stage.GetSpawnInfos("Zombie"));

        // Crate 생성
        crateManager.Init();

        // FuelBarrel 생성
        fuelBarrelManager.Init();

        // Stuff 생성
        Stuff[][] stuffs = stuffManager.Init(stage.GetSpawnInfos("Stuff"));

        // Sector 생성
        sector.SpawnSector(Vector2Int.zero, stuffs);

        Destroy(selectStageManager);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponInfoByLevel
    {
        public WeaponInfo[] weaponInfos;
    }

    public Player player;
    public ZombieManager zombieManager;

    public GameObject weaponPrefab;
    public WeaponInfoByLevel[] weaponInfoByLevels;
    private Dictionary<string, WeaponInfo> weaponInfoDic = new Dictionary<string, WeaponInfo>();

    private void Awake()
    {
        for (int i = 0; i < weaponInfoByLevels.Length; i++)
        {
            WeaponInfo[] weaponInfos = weaponInfoByLevels[i].weaponInfos;
            for (int j = 0; j < weaponInfos.Length; j++)
                weaponInfoDic.Add(weaponInfos[j].weaponName, weaponInfos[j]);
        }
    }

    public void Init()
    {
        // 게임 시작 시 Pistol을 기본 무기로 제공
        player.weapons[0] = GetWeapon(weaponInfoDic["Pistol"]);
    }

    public Weapon GetWeapon(WeaponInfo info)
    {
        IDamage baseDamage = Instantiate(info.baseDamage);
        baseDamage.Init(info.cc, info.bloodEffectName);

        IDamageTiming baseDamageTiming = Instantiate(info.baseDamageTiming);
        baseDamageTiming.Init(baseDamage, info.bulletName, info.GetStat("Range").Value);

        IGetTarget baseGetTarget = Instantiate(info.baseGetTarget);
        baseGetTarget.Init(baseDamageTiming, zombieManager.GetTarget, PoolingManager.instance.Get(info.fireEffectName, player.transform));

        Weapon script = Instantiate(weaponPrefab, player.transform).GetComponent<Weapon>();        
        script.Init(info, baseGetTarget);

        return script;
    }

    #region RandomWeapon
    [System.Serializable]
    public struct RandomWeapon
    {
        public int point;
        public int[] percents;
    }

    public RandomWeapon[] randomWeapons;

    public bool GetRandomWeapon(int index, int point, out WeaponInfo info)
    {
        info = null;

        if (point >= randomWeapons[index].point)
        {
            int random = Random.Range(0, 100);

            int percent = 0;
            for (int i = 0; i < randomWeapons[index].percents.Length; i++)
            {
                percent += randomWeapons[index].percents[i];
                if (random < percent)
                {
                    var weaponInfos = weaponInfoByLevels[i].weaponInfos;
                    info = weaponInfos[Random.Range(0, weaponInfos.Length)];
                    return true;
                }
            }

            // 여기까지 오면 문제있는거
            Debug.LogError("Error");
            return false;
        }
        else return false;
    }
    #endregion
}

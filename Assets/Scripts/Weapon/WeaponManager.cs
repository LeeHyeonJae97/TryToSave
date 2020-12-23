using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Player player;
    public ZombieManager zombieManager;

    public GameObject weaponPrefab;
    public WeaponInfo[] weaponInfos;
    private Dictionary<int, WeaponInfo> weaponInfoDic = new Dictionary<int, WeaponInfo>();

    private void Awake()
    {
        for (int i = 0; i < weaponInfos.Length; i++)
            weaponInfoDic.Add(weaponInfos[i].id, weaponInfos[i]);

        player.weapons.Add(GetWeapon(1));
    }

    public Weapon GetWeapon(int weaponId)
    {
        GameObject weapon = Instantiate(weaponPrefab, player.transform);
        WeaponInfo info = weaponInfoDic[weaponId];

        IDebuff debuff;
        switch (info.debuff)
        {
            case "JustDamage":
                debuff = new JustDamage();
                break;
            case "WithDebuff":
                debuff = new WithDebuff(info.debuffName);
                break;
            default:
                Debug.LogError("Error");
                return null;
        }

        IDamageTiming damageTiming;
        switch (info.timing)
        {
            case "HitBullet":
                damageTiming = new HitBullet(info.bulletName, debuff);
                break;
            case "RightAfterShoot":
                damageTiming = new RightAfterShoot(debuff);
                break;
            default:
                Debug.LogError("Error");
                return null;
        }

        IGetTarget getTarget;
        switch (info.target)
        {
            case "JustTarget":
                getTarget = new JustTarget(zombieManager.GetTarget, damageTiming);
                break;
            case "LinearTarget":
                getTarget = new LinearTarget(zombieManager.GetTarget, damageTiming);
                break;
            case "RangeTarget":
                getTarget = new RangeTarget(zombieManager.GetTarget, damageTiming);
                break;
            default:
                Debug.LogError("Error");
                return null;
        }

        Weapon script = weapon.GetComponent<Weapon>();
        script.Init(info, getTarget, damageTiming, debuff);

        return script;
    }
}

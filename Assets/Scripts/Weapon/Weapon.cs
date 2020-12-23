using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Id { get; private set; }
    public string WeaponName { get; private set; }
    public string BulletName { get; private set; }
    public string DebuffName { get; private set; }
    public int MaxLevel { get; private set; }

    private Dictionary<string, Stat> statDic = new Dictionary<string, Stat>();
    private float curCooldown;

    private IDebuff debuff;
    private IDamageTiming damageTiming;
    private IGetTarget getTarget;

    private void Update()
    {
        if (curCooldown >= statDic["Cooldown"].Value)
        {
            bool success = getTarget.Damage((int)statDic["MaxTarget"].Value, statDic["Damage"].Value, statDic["Range"].Value);
            if (success) curCooldown = 0;
        }
        else curCooldown += Time.deltaTime;
    }

    public void Init(WeaponInfo info, IGetTarget getTarget, IDamageTiming damageTiming, IDebuff debuff)
    {
        Id = info.id;
        WeaponName = info.weaponName;
        BulletName = info.bulletName;
        DebuffName = info.debuffName;
        MaxLevel = info.maxLevel;

        for (int i = 0; i < info.stats.Length; i++)
        {
            Stat stat = info.stats[i];
            stat.Level = 1;
            statDic.Add(stat.name, stat);
        }

        this.debuff = debuff;
        this.damageTiming = damageTiming;
        this.getTarget = getTarget;
    }
}

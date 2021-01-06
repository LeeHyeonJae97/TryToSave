using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string WeaponName { get; private set; }
    public Sprite WeaponImage { get; private set; }
    private IGetTarget baseGetTarget;
    private Dictionary<string, Stat> statDic = new Dictionary<string, Stat>();
    private float curCooldown;

    private void Update()
    {
        if (curCooldown >= statDic["Cooldown"].Value)
        {
            bool success = baseGetTarget.GetTarget((int)statDic["Damage"].Value, statDic["Range"].Value, statDic["HitRange"].Value);
            if (success) curCooldown = 0;
        }
        else curCooldown += Time.deltaTime;
    }

    public void Init(WeaponInfo info, IGetTarget baseGetTarget)
    {
        WeaponName = info.weaponName;
        WeaponImage = info.weaponImage;
        this.baseGetTarget = baseGetTarget;

        for (int i = 0; i < info.stats.Length; i++)
            statDic.Add(info.stats[i].statName, info.stats[i]);
    }

    public void GetAllStats(out Stat[] values)
    {
        values = new Stat[statDic.Values.Count];
        statDic.Values.CopyTo(values, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string WeaponName { get; private set; }
    public Sprite WeaponImage { get; private set; }
    private IGetTarget baseGetTarget;
    private GameObject fireEffect;
    private Dictionary<string, Stat> statDic;
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

    public void Init(WeaponInfo info, IGetTarget baseGetTarget, GameObject fireEffect)
    {
        WeaponName = info.weaponName;
        WeaponImage = info.weaponImage;        
        this.baseGetTarget = baseGetTarget;
        this.fireEffect = fireEffect;

        statDic = new Dictionary<string, Stat>();
        for (int i = 0; i < info.stats.Length; i++)
            statDic.Add(info.stats[i].statName, new Stat(info.stats[i].statName, info.stats[i].Values));
    }

    public void GetAllStats(out Stat[] values)
    {
        values = new Stat[statDic.Values.Count];
        statDic.Values.CopyTo(values, 0);
    }

    public void Return()
    {
        PoolingManager.instance.Return(fireEffect);
        PoolingManager.instance.Return(gameObject);
    }
}

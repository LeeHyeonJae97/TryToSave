using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObject/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public string bulletName;
    public CrowdControl cc;
    public string fireEffectName;
    public string bloodEffectName;
    public Sprite weaponImage;

    [Tooltip("JustDamage / WithCrowdControl")]
    public IDamage baseDamage;
    [Tooltip("RightAfterShoot / HitBullet")]
    public IDamageTiming baseDamageTiming;
    [Tooltip("JustTarget / LinearTarget / RangeTarget")]
    public IGetTarget baseGetTarget;

    public Stat[] stats;

    public Stat GetStat(string statName)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (statName.CompareTo(stats[i].statName) == 0)
                return stats[i];
        }

        Debug.LogError("Error");
        return null;
    }
}

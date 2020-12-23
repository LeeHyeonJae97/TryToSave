using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObject/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public int id;
    public string weaponName;
    public string bulletName;
    public string debuffName;
    public int maxLevel;

    [Tooltip("JustDamage / WithDebuff")]
    public string debuff;
    [Tooltip("RightAfterShoot / HitBullet")]
    public string timing;
    [Tooltip("JustTarget / LinearTarget / RangeTarget")]
    public string target;

    public Stat[] stats;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LinearTarget", menuName = "ScriptableObject/Weapon/LinearTarget")]
public class LinearTarget : IGetTarget
{
    public override bool GetTarget(int damage, float range, float hitRange)
    {
        if (damage == 0 || range == 0)
        {
            Debug.LogError("Error");
            return false;
        }

        GameObject target = getClosestTarget(range);
        if (target != null)
        {
            Vector3 dir = target.transform.position - Player.Pos;
            dir.y = 0;
            RaycastHit[] hits = Physics.RaycastAll(Player.Pos + Vector3.up * 0.25f, dir.normalized, range, 1 << LayerMask.NameToLayer("Zombie"));

            GameObject[] targets = new GameObject[hits.Length];
            for (int i = 0; i < targets.Length; i++) targets[i] = hits[i].collider.gameObject;

            Fire(target.transform.position);
            damageTiming.Damage(targets, damage);

            return true;
        }
        else return false;
    }
}

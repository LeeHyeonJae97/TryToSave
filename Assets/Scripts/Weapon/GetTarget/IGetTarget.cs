using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetTarget
{
    bool Damage(int amount, float damage, float range);
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTiming
{
    void Damage(GameObject target, float damage);
    void Damage(GameObject[] targets, float damage);
}
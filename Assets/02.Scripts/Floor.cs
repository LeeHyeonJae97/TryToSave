﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.position = player.position;
    }
}
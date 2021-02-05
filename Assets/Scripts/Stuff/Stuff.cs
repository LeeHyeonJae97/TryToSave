using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff
{
    public bool active;
    public int keyIndex;
    public Vector3 pos;
    public int angle;
    public GameObject go;

    public Stuff(int keyIndex, Vector3 pos, int angle)
    {
        active = false;
        this.keyIndex = keyIndex;
        this.pos = pos;
        this.angle = angle;
    }
}

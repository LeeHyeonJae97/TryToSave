using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyGizmos
{
    public static void DrawCircle(Vector3 center, Color color, float range)
    {
        Gizmos.color = color;

        for (int i = 0; i < 20; i++)
        {
            float rad = Mathf.Deg2Rad * i * 18;
            float radNext = Mathf.Deg2Rad * (i + 1) * 18;

            Vector3 pos = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * range;
            Vector3 posNext = new Vector3(Mathf.Cos(radNext), 0, Mathf.Sin(radNext)) * range;

            Gizmos.DrawRay(center + pos, posNext - pos);
        }
    }
}

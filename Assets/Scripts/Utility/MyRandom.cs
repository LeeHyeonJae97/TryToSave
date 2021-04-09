using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyRandom
{
    public static int Random(SpawnInfo[] spawnInfos)
    {
        int random = UnityEngine.Random.Range(0, 100);

        int percent = 0;
        for (int i = 0; i < spawnInfos.Length; i++)
        {
            percent += spawnInfos[i].percent;

            if (percent > 100)
            {
                Debug.LogError("Error");
                return -1;
            }

            if (random < percent) return i;
        }

        Debug.LogError("Error");
        return -1;
    }

    public static int Random(LevelInfo[] levelInfos)
    {
        int random = UnityEngine.Random.Range(0, 100);

        int percent = 0;
        for (int i = 0; i < levelInfos.Length; i++)
        {
            percent += levelInfos[i].percent;

            if (percent > 100)
            {
                Debug.LogError("Error");
                return -1;
            }

            if (random < percent) return i;
        }

        Debug.LogError("Error");
        return -1;
    }

    public static int Random(int[] percents)
    {
        int random = UnityEngine.Random.Range(0, 100);

        int percent = 0;
        for (int i = 0; i < percents.Length; i++)
        {
            percent += percents[i];

            if (percent > 100)
            {
                Debug.LogError("Error");
                return -1;
            }

            if (random < percent) return i;
        }

        Debug.LogError("Error");
        return -1;
    }
}

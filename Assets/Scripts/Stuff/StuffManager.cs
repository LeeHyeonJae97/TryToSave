using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    private const int maxRange = 14;

    public Transform player;

    private Transform holder;
    private List<Stuff> stuffs = new List<Stuff>();

    private StageTable stageTable;
    private string curStage;

    private void Awake()
    {
        holder = new GameObject("StuffHolder").transform;
    }

    private void Update()
    {
        for (int i = 0; i < stuffs.Count; i++)
        {
            if (!stuffs[i].active && (player.position - stuffs[i].pos).sqrMagnitude <= maxRange * maxRange)
            {
                GameObject stuff = stageTable.GetStuff(curStage, stuffs[i].keyIndex);
                stuff.transform.position = stuffs[i].pos;
                stuff.transform.SetParent(holder);

                stuffs[i].active = true;
                stuffs[i].go = stuff;
            }
            else if (stuffs[i].active && (player.position - stuffs[i].pos).sqrMagnitude > maxRange * maxRange)
            {
                if (stuffs[i].go == null)
                {
                    Debug.LogError("Stuff is null");
                    return;
                }

                stageTable.ReturnStuff(curStage, stuffs[i].go);

                stuffs[i].active = false;
                stuffs[i].go = null;
            }
        }
    }

    public Stuff[][] Init(StageTable stageTable, string curStage, int keyLength)
    {
        this.stageTable = stageTable;
        this.curStage = curStage;

        return SpawnStuff(keyLength);
    }

    private Stuff[][] SpawnStuff(int keyLength)
    {
        Stuff[][] stuffs = new Stuff[Sector.count * Sector.count][];
        for (int i = 0; i < stuffs.Length; i++)
        {
            List<Vector2> stuffPoses = new List<Vector2>();
            for (int j = 0; j < 10; j++)
            {
                int x = Random.Range(-Sector.halfLength + 1, Sector.halfLength);
                int y = Random.Range(-Sector.halfLength + 1, Sector.halfLength);
                Vector2 tmp = new Vector2(x, y);

                if (!stuffPoses.Contains(tmp)) stuffPoses.Add(tmp);
            }

            stuffs[i] = new Stuff[stuffPoses.Count];
            for (int j = 0; j < stuffs[i].Length; j++)
            {
                int keyIndex = Random.Range(0, keyLength);
                Stuff stuff = new Stuff(keyIndex, new Vector3(stuffPoses[j].x, 0, stuffPoses[j].y));
                stuffs[i][j] = stuff;

                this.stuffs.Add(stuff);
            }
        }

        return stuffs;
    }
}

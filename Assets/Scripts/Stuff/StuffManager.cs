using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    public int maxActiveRange = 14;

    private SpawnInfo[] spawnInfos;
    private Transform holder;
    private List<Stuff> stuffs = new List<Stuff>();

    private void Awake()
    {
        holder = new GameObject("StuffHolder").transform;
    }

    private void Update()
    {
        for (int i = 0; i < stuffs.Count; i++)
        {
            float dist = (Player.Pos - stuffs[i].pos).sqrMagnitude;

            // 일정 거리 이상 멀어지면 오브젝트를 풀에 반환
            if (stuffs[i].active && dist > maxActiveRange * maxActiveRange)
            {
                if (stuffs[i].go == null)
                {
                    Debug.LogError("Stuff is null");
                    return;
                }

                PoolingManager.instance.Return(stuffs[i].go);

                stuffs[i].active = false;
                stuffs[i].go = null;
            }

            // 일정 거리 내로 들어오면 오브젝트 할당받아 활성화
            else if (!stuffs[i].active && dist <= maxActiveRange * maxActiveRange)
            {
                GameObject go = PoolingManager.instance.Get(spawnInfos[stuffs[i].keyIndex].name, holder,
                    stuffs[i].pos, Quaternion.Euler(0, stuffs[i].angle, 0));

                stuffs[i].active = true;
                stuffs[i].go = go;
            }
        }
    }

    // Stuff[Sector 개수][Sector에 있는 Stuff의 개수]
    public Stuff[][] Init(SpawnInfo[] spawnInfos)
    {
        this.spawnInfos = spawnInfos;

        Stuff[][] stuffs = new Stuff[Sector.count * Sector.count][];
        for (int i = 0; i < stuffs.Length; i++)
        {
            // 위치
            List<Vector2> stuffPoses = new List<Vector2>();
            for (int j = 0; j < 10; j++)
            {
                int x = Random.Range(-Sector.halfLength + 1, Sector.halfLength);
                int y = Random.Range(-Sector.halfLength + 1, Sector.halfLength);
                Vector2 tmp = new Vector2(x, y);

                if (!stuffPoses.Contains(tmp)) stuffPoses.Add(tmp);
            }

            // 회전 각도
            int angle = Random.Range(0, 360);

            stuffs[i] = new Stuff[stuffPoses.Count];
            for (int j = 0; j < stuffs[i].Length; j++)
            {
                int index = MyRandom.Random(spawnInfos);
                if (index > -1)
                {
                    Stuff stuff = new Stuff(index, new Vector3(stuffPoses[j].x, 0, stuffPoses[j].y), angle);
                    stuffs[i][j] = stuff;

                    this.stuffs.Add(stuff);
                }
            }
        }

        return stuffs;
    }

    private void OnDrawGizmosSelected()
    {
        MyGizmos.DrawCircle(Player.Pos, Color.blue, maxActiveRange);
    }
}

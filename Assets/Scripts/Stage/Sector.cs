using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{   
    public const int count = 7;
    public const int halfCount = 3;
    public const int length = 30;
    public const int halfLength = 15;

    private Dictionary<Vector2Int, Stuff[]> sectorDic = new Dictionary<Vector2Int, Stuff[]>();    

    // 좌측하단부터 순서대로 우측상단까지 정렬
    // 넘겨받은 Stuff들의 위치를 Sector를 고려하여 설정한뒤 sectorDic에 추가
    public void SpawnSector(Vector2Int curSector, Stuff[][] stuffs)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Vector2Int tmp = new Vector2Int(curSector.x - halfCount + j, curSector.y - halfCount + i);
                for (int k = 0; k < stuffs[i * count + j].Length; k++)
                    stuffs[i * count + j][k].pos += new Vector3(tmp.x, 0, tmp.y) * length;

                sectorDic.Add(tmp, stuffs[i * count + j]);
            }
        }

        //Debug.Log(curSector);
    }

    // 기존에 설정되어있던 Sector범위를 벗어난 경우 현재 Sector를 중심으로 재정렬
    // 현재 위치를 중심으로 반대편(멀리 떨어져있는 쪽) Sector의 정보를
    // 그대로 가져와 새롭게 설정해줘야하는 Sector에 사용
    public void Rearrange(Vector2Int curSector, Vector2Int newSector)
    {
        for (int y = newSector.y - halfCount; y <= newSector.y + halfCount; y++)
        {
            for (int x = newSector.x - halfCount; x <= newSector.x + halfCount; x++)
            {
                Vector2Int tmp = new Vector2Int(x, y);

                if (!sectorDic.ContainsKey(tmp))
                {
                    int xDiff = tmp.x - newSector.x;
                    int yDiff = tmp.y - newSector.y;
                    Vector2Int org = new Vector2Int(curSector.x - xDiff, curSector.y - yDiff);

                    Stuff[] stuffs = sectorDic[org];
                    for (int i = 0; i < stuffs.Length; i++)
                        stuffs[i].pos = stuffs[i].pos + new Vector3(-org.x + tmp.x, 0, -org.y + tmp.y) * length;

                    sectorDic.Add(tmp, stuffs);
                    sectorDic.Remove(org);
                }
            }
        }

        //Debug.Log(curSector + "  " + newSector);
    }
}

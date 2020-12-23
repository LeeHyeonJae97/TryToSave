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
                        stuffs[i].pos = stuffs[i].pos + new Vector3(-org.x + tmp.x, 0, -org.y + tmp.y) * 30;
                    sectorDic.Add(tmp, stuffs);
                    sectorDic.Remove(org);
                }
            }
        }

        Debug.Log(curSector + "  " + newSector);
    }
}

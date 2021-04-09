using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObject/Stage")]
public class Stage : ScriptableObject
{
    [System.Serializable]
    public class SpawnObject
    {
        public string name;
        public int percent;
        public GameObject prefab;
    }

    public string stageName;
    public Sprite preview;
    public Material groundMaterial;
    public SpawnObject[] zombies;
    public SpawnObject[] stuffs;
    
    public SpawnInfo[] GetSpawnInfos(string type)
    {
        SpawnObject[] spawnObjects;
        switch (type)
        {
            case "Zombie":
                spawnObjects = zombies;
                break;
            case "Stuff":
                spawnObjects = stuffs;
                break;
            default:
                return null;
        }

        SpawnInfo[] spawnInfos = new SpawnInfo[spawnObjects.Length];
        for (int i = 0; i < spawnInfos.Length; i++)
            spawnInfos[i] = new SpawnInfo(spawnObjects[i].name, spawnObjects[i].percent);

        return spawnInfos;
    }
}

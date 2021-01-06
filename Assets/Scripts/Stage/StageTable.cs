using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTable : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        [System.Serializable]
        public class SpawnObject
        {
            public string name;
            public int percent;
            public GameObject prefab;
        }

        public string name;
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

        public string[] GetZombieNames()
        {
            string[] names = new string[zombies.Length];
            for (int i = 0; i < names.Length; i++)
                names[i] = zombies[i].name;

            return names;
        }

        public string[] GetStuffNames()
        {
            string[] names = new string[stuffs.Length];
            for (int i = 0; i < names.Length; i++)
                names[i] = stuffs[i].name;

            return names;
        }
    }

    public Stage[] stages;
    private Dictionary<string, Stage> stageDic = new Dictionary<string, Stage>();

    private void Awake()
    {
        for (int i = 0; i < stages.Length; i++)
            stageDic.Add(stages[i].name, stages[i]);
    }

    public Stage Get(string key)
    {
        return stageDic[key];
    }
}

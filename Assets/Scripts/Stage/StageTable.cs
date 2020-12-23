using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTable : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        [System.Serializable]
        public class Prefab
        {
            public string key;
            public GameObject prefab;
        }

        public string key;
        public Prefab[] zombies;
        public Prefab[] stuffs;

        public string[] GetZombieKeys()
        {
            string[] keys = new string[zombies.Length];
            for (int i = 0; i < keys.Length; i++)
                keys[i] = zombies[i].key;

            return keys;
        }
    }

    public Stage[] stages;
    private Dictionary<string, Stage> stageDic = new Dictionary<string, Stage>();

    private void Awake()
    {
        for (int i = 0; i < stages.Length; i++)
            stageDic.Add(stages[i].key, stages[i]);
    }

    public Stage Get(string key)
    {
        return stageDic[key];
    }

    public GameObject GetStuff(string key, int keyIndex)
    {
        if (!stageDic.ContainsKey(key))
        {
            Debug.LogError("Wrong stage name");
            return null;
        }

        return PoolingManager.instance.Get(stageDic[key].stuffs[keyIndex].key);
    }

    public void ReturnStuff(string key, GameObject stuff)
    {
        if(!stageDic.ContainsKey(key))
        {
            Debug.LogError("Wrong stage name");
            return;
        }

        PoolingManager.instance.Return(stuff);
    }
}

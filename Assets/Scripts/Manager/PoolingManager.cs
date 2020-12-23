using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public int initAmount;
        public GameObject prefab;
        private Transform holder;
        private Queue<GameObject> queue;

        public Pool(string key, int initAmount, GameObject prefab)
        {
            this.key = key;
            this.initAmount = initAmount;
            this.prefab = prefab;

            Init();
        }

        public void Init()
        {
            queue = new Queue<GameObject>();
            if (holder == null) holder = new GameObject(key + "Holder").transform;            

            for (int i = 0; i < initAmount; i++)
            {
                GameObject go = Instantiate(prefab, holder);
                go.transform.SetParent(holder);
                go.name = key;
                go.SetActive(false);
                queue.Enqueue(go);
            }
        }

        public GameObject Get()
        {
            if (queue.Count <= 0) Init();

            GameObject go = queue.Dequeue();
            go.SetActive(true);
            return go;
        }

        public void Return(GameObject go)
        {
            go.transform.SetParent(holder);
            go.SetActive(false);
            queue.Enqueue(go);
        }
    }

    public static PoolingManager instance;

    public Pool[] pools;
    private Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Debug.LogError("More than one PoolingManagers are in Scene");
            return;
        }

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].Init();
            poolDic.Add(pools[i].key, pools[i]);
        }
    }

    public void CreatePool(string key, int initAmount, GameObject prefab)
    {
        Pool pool = new Pool(key, initAmount, prefab);
        poolDic.Add(key, pool);
    }

    #region Get
    public GameObject Get(string key)
    {
        if(!poolDic.ContainsKey(key))
        {
            Debug.LogError("Wrong key");
            return null;
        }

        GameObject go = poolDic[key].Get();
        go.transform.SetParent(null);
        return go;
    }

    public GameObject Get(string key, Transform parent)
    {
        if (!poolDic.ContainsKey(key))
        {
            Debug.LogError("Wrong key");
            return null;
        }

        GameObject go = poolDic[key].Get();
        go.transform.SetParent(parent);
        return go;
    }

    public GameObject Get(string key, Vector3 pos)
    {
        if (!poolDic.ContainsKey(key))
        {
            Debug.LogError("Wrong key");
            return null;
        }

        GameObject go = poolDic[key].Get();
        go.transform.SetParent(null);
        go.transform.position = pos;
        return go;
    }

    public GameObject Get(string key, Transform parent, Vector3 pos)
    {
        if (!poolDic.ContainsKey(key))
        {
            Debug.LogError("Wrong key");
            return null;
        }

        GameObject go = poolDic[key].Get();
        go.transform.SetParent(parent);
        go.transform.position = pos;
        return go;
    }
    #endregion

    #region Return
    public void Return(GameObject go)
    {
        poolDic[go.name].Return(go);
    }
    #endregion
}

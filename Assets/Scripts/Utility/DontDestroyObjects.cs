using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyObjects : MonoBehaviour
{
    public static DontDestroyObjects instance;

    public GameObject selectStageManager;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        DontDestroyOnLoad(selectStageManager);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Debug.Log(gameObject.GetInstanceID());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.CompareTo("GameScene") == 0)
        {
            //Destroy(this);
        }
    }
}

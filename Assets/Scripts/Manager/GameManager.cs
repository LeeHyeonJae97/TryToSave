using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public StageManager stageManager;
    public WeaponManager weaponManager;

    private void Start()
    {
        Loading.instance.StartLoading(() => Timer.instance.SetTimer(3, () => Time.timeScale = 1));

        weaponManager.Init();
        stageManager.Init();

        Time.timeScale = 0;
    }
}

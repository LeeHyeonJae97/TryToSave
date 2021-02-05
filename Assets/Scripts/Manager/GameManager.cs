using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public StageManager stageManager;
    public WeaponManager weaponManager;
    public Player player;

    public GameObject gameOverCanvas;

    public Text timeText;
    private float time;
    private bool goingOn;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(goingOn)
        {
            time += Time.deltaTime;
            int integerTime = Mathf.FloorToInt(time);

            timeText.text = string.Format("{0:D2}:{1:D2}", integerTime / 60, integerTime % 60);
        }
    }

    private void StartGame()
    {
        Loading.instance.StartLoading(3, () => Timer.instance.SetTimer(3, () =>
        {
            stageManager.StartSpawn();
            goingOn = true;
        }));

        weaponManager.Init();
        stageManager.Init();
    }

    public void RestartGame()
    {
        Loading.instance.StartLoading(5, () => Timer.instance.SetTimer(3, () =>
        {
            stageManager.StartSpawn();
            goingOn = true;
        }));

        stageManager.Reset();
        player.Reset();
        weaponManager.Init();

        timeText.text = "00:00";
        time = 0;
        goingOn = false;
    }

    public void GameOver()
    {
        goingOn = false;
        gameOverCanvas.SetActive(true);
    }
}

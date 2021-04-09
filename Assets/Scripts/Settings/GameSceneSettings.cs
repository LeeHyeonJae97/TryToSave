using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneSettings : SettingsManager
{
    public GameManager gameManager;
    public Joystick joystick;

    private void Start()
    {
        filePath = Application.persistentDataPath + filePath;

        Load();

        gameObject.SetActive(false);
    }

    public override void Load()
    {
        base.Load();
        joystick.SetJoystickPos(settings.joystickPosIndex);
    }

    public override void SetJoystickPos(int index)
    {
        base.SetJoystickPos(index);
        joystick.SetJoystickPos(index);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Timer.instance.SetTimer(3, () => Time.timeScale = 1);
    }

    public void Retry()
    {
        AlertConfirmPanel.instance.Confirm("Really want to retry? You will lose everything in this game", () =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            gameManager.RestartGame();
        });
    }

    public void BackToTitleScene()
    {
        AlertConfirmPanel.instance.Confirm("Really want to go to title? You will lose everything in this game", () =>
        {
            Loading.instance.StartLoading(3, null);

            gameObject.SetActive(false);
            Time.timeScale = 1;

            SceneManager.LoadScene("TitleScene");
        });
    }
}

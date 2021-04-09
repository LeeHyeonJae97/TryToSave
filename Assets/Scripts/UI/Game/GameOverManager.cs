using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameManager gameManager;

    public void Retry()
    {
        gameObject.SetActive(false);

        gameManager.RestartGame();
    }

    public void BackToTitleScene()
    {
        Loading.instance.StartLoading(3, null);

        gameObject.SetActive(false);

        SceneManager.LoadScene("TitleScene");
    }
}

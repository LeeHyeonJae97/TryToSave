using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject settingsCanvas;

    public void Pause()
    {
        Time.timeScale = 0;
        settingsCanvas.SetActive(true);
    }
}

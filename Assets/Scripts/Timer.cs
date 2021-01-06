using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public Text timerText;
    public RectTransform timerTr;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (this != instance)
        {
            Debug.LogError("Error");
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }

    public void SetTimer(float time, UnityAction onFinishTimer)
    {
        gameObject.SetActive(true);
        StartCoroutine(Countdown(time, onFinishTimer));
    }

    private IEnumerator Countdown(float time, UnityAction onFinishTimer)
    {
        while (time > 0)
        {
            timerText.text = ((int)time).ToString();
            timerTr.localScale *= 0.9f;
            time -= Time.deltaTime;

            yield return null;
        }

        if(onFinishTimer != null) onFinishTimer.Invoke();
        gameObject.SetActive(false);
    }
}

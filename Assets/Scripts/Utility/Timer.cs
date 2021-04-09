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
        else
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }

    public void SetTimer(float time, UnityAction onFinish)
    {
        gameObject.SetActive(true);
        StartCoroutine(Countdown(time, onFinish));
    }

    private IEnumerator Countdown(float time, UnityAction onFinish)
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);

        int curTime = (int)time;
        for (int i = 0; i < time; i++)
        {
            timerText.text = curTime.ToString();
            curTime -= 1;

            yield return wait;
        }

        if (onFinish != null) onFinish.Invoke();

        gameObject.SetActive(false);
    }

    /*
    private IEnumerator Countdown(float time, UnityAction onFinish)
    {
        float waitSec = 0.02f;
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(waitSec);
        Debug.Log(wait.waitTime); 

        int integerTime = 0;
        while (time > 0)
        {
            //Debug.Log(time);

            int curIntergerTime = Mathf.CeilToInt(time);
            if (integerTime != curIntergerTime)
            {
                integerTime = curIntergerTime;
                timerText.text = curIntergerTime.ToString();
                timerTr.localScale = Vector3.one;
            }

            timerTr.localScale *= 0.97f;
            time -= waitSec;

            yield return wait;
        }

        if(onFinish != null) onFinish.Invoke();

        gameObject.SetActive(false);
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Loading : MonoBehaviour
{
    public static Loading instance;

    public Text loadingText;
    private string[] texts = { "Loading.", "Loading..", "Loading..." };
  
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartLoading(3, null);
    }

    public void StartLoading(int time, UnityAction onFinished)
    {
        gameObject.SetActive(true);

        StartCoroutine(CorLoading(time, onFinished));
        StartCoroutine(CorLoadingText());
    }

    private IEnumerator CorLoading(int time, UnityAction onFinished)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);

        StopAllCoroutines();

        if (onFinished != null) onFinished.Invoke();
    }

    private IEnumerator CorLoadingText()
    {
        WaitForSecondsRealtime interval = new WaitForSecondsRealtime(0.5f);
        int index = 0;

        while (true)
        {
            index += 1;
            if (index >= texts.Length) index = 0;

            loadingText.text = texts[index];

            yield return interval;
        }
    }
}

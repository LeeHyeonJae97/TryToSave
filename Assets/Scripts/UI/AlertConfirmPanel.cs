using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AlertConfirmPanel : MonoBehaviour
{
    public Text message;
    public RectTransform confirmButton;
    public RectTransform cancelButton;
    private UnityAction onClickConfirmButton;

    private void Awake()
    {
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void Alert(string message)
    {
        gameObject.SetActive(true);

        this.message.text = message;

        cancelButton.gameObject.SetActive(true);

        cancelButton.anchoredPosition = new Vector2(0, -70);
    }

    public void Alert(string message, UnityAction onClick)
    {
        gameObject.SetActive(true);

        this.message.text = message;

        onClickConfirmButton = onClick;

        confirmButton.gameObject.SetActive(true);

        confirmButton.anchoredPosition = new Vector2(0, -70);
    }

    public void Confirm(string message, UnityAction onClick)
    {
        gameObject.SetActive(true);

        this.message.text = message;

        onClickConfirmButton = onClick;

        confirmButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        confirmButton.anchoredPosition = new Vector2(-100, -70);
        cancelButton.anchoredPosition = new Vector2(100, -70);
    }

    public void OnClickConfirmButton()
    {
        OnClickCancelButton();
        if (onClickConfirmButton != null) onClickConfirmButton.Invoke();
        else Debug.LogError("Error");
    }

    public void OnClickCancelButton()
    {
        gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }
}

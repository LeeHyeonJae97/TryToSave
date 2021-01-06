using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AlertConfirmPanel : MonoBehaviour
{
    public Vector2 center;
    public Vector2 left, right;

    public static AlertConfirmPanel instance;

    public Text message;
    public RectTransform confirmButton;
    public RectTransform cancelButton;
    private UnityAction onClickConfirmButton;

    private void Awake()
    {
        if (instance == null) instance = this;

        else if (this != instance)
        {
            Debug.LogError("Error");
            Destroy(this);
            return;
        }

        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Alert(string message)
    {
        gameObject.SetActive(true);

        this.message.text = message;

        cancelButton.gameObject.SetActive(true);
        cancelButton.anchoredPosition = center;
    }

    public void Alert(string message, UnityAction onClick)
    {
        gameObject.SetActive(true);

        this.message.text = message;
        onClickConfirmButton = onClick;

        confirmButton.gameObject.SetActive(true);
        confirmButton.anchoredPosition = center;
    }

    public void Confirm(string message, UnityAction onClick)
    {
        gameObject.SetActive(true);

        this.message.text = message;
        onClickConfirmButton = onClick;

        confirmButton.gameObject.SetActive(true);
        confirmButton.anchoredPosition = left;
        cancelButton.gameObject.SetActive(true);
        cancelButton.anchoredPosition = right;
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

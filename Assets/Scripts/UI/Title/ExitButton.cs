using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        AlertConfirmPanel.instance.Confirm("Really want to exit game?", () => Application.Quit());
    }
}

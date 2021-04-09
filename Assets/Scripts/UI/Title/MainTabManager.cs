using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTabManager : MonoBehaviour
{
    public GameObject[] tabs;
    private GameObject curTab;    

    public void ClickTabButton(int index)
    {
        if (curTab == tabs[index]) return;

        if (curTab != null) curTab.SetActive(false);

        tabs[index].SetActive(true);
        curTab = tabs[index];
    }

    /*
    public void CloseTab()
    {
        if (curTab == null)
        {
            Debug.LogError("Error");
            return;
        }

        curTab.SetActive(false);
        curTab = null;
    }
    */
}

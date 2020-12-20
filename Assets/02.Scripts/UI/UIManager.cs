using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
    public RectTransform[] uis;

    public void ChangeHand()
    {
        for (int i = 0; i < uis.Length; i++)
        {
            Vector3 pos = uis[i].anchoredPosition3D;
            uis[i].anchoredPosition3D = new Vector3(-pos.x, pos.y, pos.z);

            if (uis[i].TryGetComponent(out Text text))
            {
                int ali = (int)text.alignment;
                if (ali % 3 == 0) text.alignment = (TextAnchor)(ali + 2);
                else if (ali % 3 == 2) text.alignment = (TextAnchor)(ali - 2);
            }

            if (uis[i].TryGetComponent(out UITween uiTween))
            {
                for (int j = 0; j < uiTween.poses.Length; j++)
                {
                    Vector2 tweenPos = uiTween.poses[j];
                    uiTween.poses[j] = new Vector2(-tweenPos.x, tweenPos.y);
                }
            }
        }
    }
    */
}

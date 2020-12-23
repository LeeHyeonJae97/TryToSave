using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweenPlanner : MonoBehaviour
{
    [System.Serializable]
    public class UITweenPlan
    {
        public UITween uiTween;
        public bool wait;
        public bool reverse; // hide
    }

    public UITweenPlan[] uiTweenPlans;

    public void Tween()
    {
        StartCoroutine(CorTween());
    }

    private IEnumerator CorTween()
    {
        for (int i = 0; i < uiTweenPlans.Length; i++)
        {
            UITweenPlan plan = uiTweenPlans[i];

            if (uiTweenPlans[i].wait)
                yield return StartCoroutine(plan.uiTween.CorTween(plan.reverse));
            else
            {
                StartCoroutine(plan.uiTween.CorTween(plan.reverse));
                yield return null;
            }
        }
    }
}

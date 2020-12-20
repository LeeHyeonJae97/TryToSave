using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public bool move;
    public bool scale;
    public bool atFirstOnStart;
    public bool showHide;
    public bool showOnStart;
    public float[] times;
    public Vector2[] poses;
    public Vector2[] sizes;

    private float velocity;
    private bool showAtFirstPos;

    private void Awake()
    {
        if (move && poses.Length > 0) transform.position = atFirstOnStart ? poses[0] : poses[poses.Length - 1];
        if (scale && sizes.Length > 0) ((RectTransform)transform).sizeDelta = atFirstOnStart ? sizes[0] : sizes[sizes.Length - 1];

        if (showHide) gameObject.SetActive(showOnStart);

        float dist = 0;
        for (int i = 0; i < poses.Length - 1; i++)
            dist += (poses[i] - poses[i + 1]).magnitude;
        //velocity = dist / time;

        showAtFirstPos = (atFirstOnStart && showOnStart) || (!atFirstOnStart && !showOnStart);
    }

    public IEnumerator CorTween(bool reverse) // reverse = hide
    {
        if(move && showHide)
        {
            if (!reverse && !showAtFirstPos)
            {
                gameObject.SetActive(true);
                for (int i = 0; i < poses.Length; i++)
                    while (!Move(poses[i])) yield return null;
            }
            else if (reverse && showAtFirstPos)
            {
                gameObject.SetActive(true);
                for (int i = poses.Length - 2; i >= 0; i--)
                    while (!Move(poses[i])) yield return null;
            }
            else if (!reverse && showAtFirstPos)
            {
                for (int i = 0; i < poses.Length; i++)
                    while (!Move(poses[i])) yield return null;
                gameObject.SetActive(false);
            }
            else if (reverse && !showAtFirstPos)
            {
                for (int i = poses.Length - 2; i >= 0; i--)
                    while (!Move(poses[i])) yield return null;
                gameObject.SetActive(false);
            }
        }
        else if(move)
        {
            if(reverse)
            {
                for (int i = poses.Length - 2; i >= 0; i--)
                    while (!Move(poses[i])) yield return null;
            }
            else
            {
                for (int i = 0; i < poses.Length; i++)
                    while (!Move(poses[i])) yield return null;
            }
        }
        else if(showHide)
        {
            if (reverse)  // hide            
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }

    private bool Move(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, velocity);
        if (((Vector2)transform.position - targetPos).sqrMagnitude < 100f)
        {
            transform.position = targetPos;
            return true;
        }
        else return false;
    }
}

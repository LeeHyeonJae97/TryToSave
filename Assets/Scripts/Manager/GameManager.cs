using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text levelText;
    public Image pointBar;
    public Text goldText;

    private int level;
    public int Level
    {
        get { return level; }

        set
        {
            level = value;
            levelText.text = level.ToString();
        }
    }
    private int point;
    public int Point
    {
        get { return point; }

        set
        {
            point = value;

            if (point >= maxPoints[level])
            {
                pointBar.fillAmount = point = 0;
                LevelUp();
            }
            else pointBar.fillAmount = (float)point / maxPoints[level];
        }
    }
    private int[] maxPoints = { 100, 150, 200, 270, 340, 450, 600, 800, 1050, 1300, 1600, 2000 };
    private int gold;
    public int Gold
    {
        get { return gold; }

        set
        {
            gold = value;
            goldText.text = gold.ToString();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }

    public void LevelUp()
    {
        Level += 1;

    }
}

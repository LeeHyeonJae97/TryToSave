using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStageManager : MonoBehaviour
{
    public Text stageName;
    public Image stagePreview;

    private int index;
    public Stage[] stages;

    private void OnEnable()
    {        
        SetStage(index = 0);
    }

    public void Select()
    {
        BetweenSceneData.selectedStage = stages[index];

        SceneManager.LoadScene("GameScene");
    }

    public void Swipe(bool left)
    {
        if (left)
        {
            index -= 1;
            if (index < 0) index = stages.Length - 1;
        }
        else
        {
            index += 1;
            if (index >= stages.Length) index = 0;
        }

        SetStage(index);
    }

    private void SetStage(int index)
    {
        stageName.text = stages[index].stageName;
        stagePreview.sprite = stages[index].preview;
    }
}


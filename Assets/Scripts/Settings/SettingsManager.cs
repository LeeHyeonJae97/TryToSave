using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingsManager : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        public float bgmVolume;
        public float sfxVolume;
        public int joystickPosIndex;

        public Settings(float bgmVolume, float sfxVolume, int joystickPosIndex)
        {
            this.bgmVolume = bgmVolume;
            this.sfxVolume = sfxVolume;
            this.joystickPosIndex = joystickPosIndex;
        }
    }

    public Slider bgmSlider;
    public Slider sfxSlider;

    public Color idle;
    public Color selected;
    public Text[] joystickPosTexts;

    protected Settings settings;
    protected string filePath = "/Settings.json";

    private void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(settings));
    }

    public virtual void Load()
    {
        //if (File.Exists(filePath)) File.Delete(filePath);

        if (File.Exists(filePath)) settings = JsonUtility.FromJson<Settings>(File.ReadAllText(filePath));
        else settings = new Settings(0.8f, 0.8f, 0);

        bgmSlider.value = settings.bgmVolume;
        AudioManager.instance.SetBgmVolume(settings.bgmVolume);

        sfxSlider.value = settings.sfxVolume;
        AudioManager.instance.SetSfxVolume(settings.sfxVolume);

        joystickPosTexts[settings.joystickPosIndex].color = selected;
    }

    public void SetBgmVolume(float volume)
    {
        settings.bgmVolume = volume;
        AudioManager.instance.SetBgmVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        settings.sfxVolume = volume;
        AudioManager.instance.SetSfxVolume(volume);
    }

    public virtual void SetJoystickPos(int index)
    {
        joystickPosTexts[index].color = selected;
        joystickPosTexts[settings.joystickPosIndex].color = idle;

        settings.joystickPosIndex = index;
    }
}

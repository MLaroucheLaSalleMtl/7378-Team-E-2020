﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    public Dropdown qualityDropdown;

    public Toggle fullscreenToggle;

    public Slider[] volSliders;

    //public Toggle autoLockToggle;
    
    Resolution[] resolutions;

    private void LoadResolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    private void LoadGFX()
        => qualityDropdown.value = QualitySettings.GetQualityLevel();

    void Start()
    {
        LoadResolution();
        LoadGFX();

        audioMixer.SetFloat("ambientVol", PlayerPrefs.GetFloat("ambientVol"));
        volSliders[0].value = PlayerPrefs.GetFloat("ambientVol");
        audioMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
        volSliders[1].value = PlayerPrefs.GetFloat("sfxVol");
        //LoadGFX();
        SetVolume(volSliders[0].value);
        SetVolumeSFX(volSliders[1].value);
    }

    public void SetQuality(int qualityIndex)
        => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SaveSetting() => PlayerPrefs.Save();

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("ambientVol", volume);
        PlayerPrefs.SetFloat("ambientVol", volume);
        SaveSetting();
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("sfxVol", volume);
        PlayerPrefs.SetFloat("sfxVol", volume);
        SaveSetting();
    }
    public void SetFullScreen(bool isFullScreen)
        => Screen.fullScreen = isFullScreen;

    //public void SetAutoLock(bool isAutoLocked)
    //{

    //}
}

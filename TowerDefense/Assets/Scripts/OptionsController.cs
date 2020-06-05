﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    string menuSceneName = "MainMenu";
    Resolution[] resolutions;

    public AudioMixer themeAudioMixer;
    public AudioMixer soundsAudioMixer;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;

    public TurretPanelUI turretPanelUI;
    public GameObject optionsCanvas;
    public GameObject controlsCanvas;

    public TMP_InputField cameraMovementEnableKeyInput;
    string buttonAudioName = "ButtonClick";

    private void Awake()
    {
        graphicsDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> resolutionsList = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsList.Add(resolutionString);

            //if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);

        optionsCanvas.SetActive(true);
        controlsCanvas.SetActive(false);

        cameraMovementEnableKeyInput.onValueChanged.AddListener(delegate
        {
            PlayerPrefs.SetString("cameraMovementEnableKey", cameraMovementEnableKeyInput.text.ToLower());
        });
    }

    public void BackToMainMenu()
    {
        SceneFader.Instance.FadeTo(menuSceneName, LoadType.Menu);
    }

    public void SetThemeVolume(float volume)
    {
        Debug.Log(volume);
        themeAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        Debug.Log(volume);
        soundsAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ShowTurretsPanel()
    {
        turretPanelUI.Show();
    }

    public void ToggleControlsCanvas()
    {
        controlsCanvas.SetActive(!controlsCanvas.activeSelf);
    }

    public void PlaySound(string s)
    {
        if (s == null) s = buttonAudioName;
        AudioManager.Instance.Play(s);
    }

}

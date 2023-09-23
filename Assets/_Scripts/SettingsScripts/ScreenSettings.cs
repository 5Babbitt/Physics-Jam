using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenSettings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        PopulateResolutionDropdown();
        
        if (PlayerPrefs.HasKey("isFullscreen"))
            LoadScreenSettings();
        else   
            SetScreenSettings();
    }
    
    public void SetFullscreen()
    {
        bool value = fullscreenToggle.isOn;

        Screen.fullScreen = value;
        PlayerPrefs.SetInt("isFullscreen", Screen.fullScreen ? 1 : 0);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = filteredResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    void SetScreenSettings()
    {
        SetFullscreen();

    }

    void LoadScreenSettings()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("isFullscreen") != 0;

        SetScreenSettings();
    }

    void PopulateResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate)
                filteredResolutions.Add(resolutions[i]);
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{filteredResolutions[i].width}x{filteredResolutions[i].height} {filteredResolutions[i].refreshRateRatio.value}Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}

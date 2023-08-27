using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public Button muteButton;
    public Button lowVolumeButton;
    public Button mediumVolumeButton;
    public Button highVolumeButton;

    private AudioSource audioSource;
    private float defaultVolume = 0.5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", defaultVolume);
        audioSource.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        
        muteButton.onClick.AddListener(MuteVolume);
        lowVolumeButton.onClick.AddListener(LowVolume);
        mediumVolumeButton.onClick.AddListener(MediumVolume);
        highVolumeButton.onClick.AddListener(HighVolume);
    }

    private void OnVolumeChanged(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    private void MuteVolume()
    {
        volumeSlider.value = 0;
        audioSource.volume = 0;
        PlayerPrefs.SetFloat("Volume", 0);
        PlayerPrefs.Save();
    }

    private void LowVolume()
    {
        volumeSlider.value = 0.2f;
        audioSource.volume = 0.2f;
        PlayerPrefs.SetFloat("Volume", 0.2f);
        PlayerPrefs.Save();
    }

    private void MediumVolume()
    {
        volumeSlider.value = 0.5f;
        audioSource.volume = 0.5f;
        PlayerPrefs.SetFloat("Volume", 0.5f);
        PlayerPrefs.Save();
    }

    private void HighVolume()
    {
        volumeSlider.value = 1.0f;
        audioSource.volume = 1.0f;
        PlayerPrefs.SetFloat("Volume", 1.0f);
        PlayerPrefs.Save();
    }
}
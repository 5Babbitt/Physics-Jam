using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class VolumeOption : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Volume Slider")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Volume Slider"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    
    [Header("Values")]
    public string optionName;
    [Range(0, 1), SerializeField] private float sliderVolume; 
    [Range(0, 100)] public int volumePercent;
    public float volumeValue;

    [Header("Object References")]
    public TMP_Text percentValueText;
    public TMP_Text optionNameText;
    public Slider volumeSlider;
    public AudioMixer mixer;

    private void Start() 
    {
        if (PlayerPrefs.HasKey(optionName))
            LoadVolume();
        else   
            SetVolume();
    }

    private void OnValidate() 
    {
        optionNameText.text = $"{optionName} Volume";
        percentValueText.text = volumePercent.ToString();
        
        volumeSlider.value = sliderVolume;
        volumeValue = Mathf.Log10(sliderVolume) * 20;
    }

    public void SetVolumePercent(float value)
    {
        sliderVolume = value;
        volumePercent = (int)(sliderVolume * 100);
        
        volumeValue = Mathf.Log10(sliderVolume) * 20;

        SetVolume();
    }

    public void SetVolume()
    {
        percentValueText.text = volumePercent.ToString();
        
        mixer.SetFloat(optionName, volumeValue);
        PlayerPrefs.SetFloat(optionName, sliderVolume);
    }

    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat(optionName);
        
        volumeSlider.value = volume;

        SetVolumePercent(volume);
    }
}

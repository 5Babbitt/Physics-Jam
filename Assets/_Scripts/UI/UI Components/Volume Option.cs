using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
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
    [Range(0, 100)] public int volumePercent;

    [Header("Object References")]
    public TMP_Text percentValueText;
    public TMP_Text optionNameText;
    public Slider volumeSlider;

    private void OnValidate() 
    {
        optionNameText.text = optionName;
        percentValueText.text = volumePercent.ToString();
        
        volumeSlider.value = volumePercent;
    }
}

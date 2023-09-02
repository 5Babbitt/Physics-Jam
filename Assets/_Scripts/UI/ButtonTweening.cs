using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTweening : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // When hovering over a button, enlarge it to show player feedback and reset size when cursor no longer over

    public Button button;
    [Range(0f, 5f)] public float duration;

    public Vector3 defaultScale;
    public Vector3 newScale;

    private void Awake() 
    {
        button = GetComponent<Button>();
        defaultScale = button.transform.localScale;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Button Hover");
        LeanTween.scale(button.gameObject, newScale, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Button Hover Exit");
        LeanTween.scale(button.gameObject, defaultScale, duration);
    }

    private void OnValidate() 
    {
        button = GetComponent<Button>();
        defaultScale = button.transform.localScale;
    }

}

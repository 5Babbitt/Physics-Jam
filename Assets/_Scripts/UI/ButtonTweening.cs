using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonTweening : MonoBehaviour
{
    // When hovering over a button, enlarge it to show player feedback and reset size when cursor no longer over

    public Button button;

    private void Awake() 
    {
        button = GetComponent<Button>();
    }

}

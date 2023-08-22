using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[ExecuteInEditMode()]
public class HollowProgressBar : ProgressBar
{
    private RectTransform rectTransform;
    
    [Header("Mark Settings")]
    public Image mark;
    public TMP_Text markerText;    
    public float height;
    public float markCurrent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        height = rectTransform.rect.height;
    }

    protected override void Update()
    {
        base.Update();
        
        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        markCurrent = mask.fillAmount * height;
        mark.rectTransform.localPosition = new Vector3(mark.rectTransform.localPosition.x, markCurrent - (height / 2), mark.rectTransform.localPosition.z);
    }

    public void SetMarkerText(string str)
    {
        markerText.text = str;
    }
}

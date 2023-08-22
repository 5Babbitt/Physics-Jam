using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode()]
public class ProgressText : MonoBehaviour
{
    private TMP_Text text;
    public ProgressBar progressBar;
    public int max;
    public int current;

    void Awake()
    {
        progressBar = GetComponentInParent<ProgressBar>();
        text = GetComponent<TMP_Text>();
    }
    
    void Start()
    {
        max = (int)progressBar.maximum;
    }

    void Update()
    {
        current = (int)progressBar.current;
        
        text.text = $"{current}/{max}";
    }
}

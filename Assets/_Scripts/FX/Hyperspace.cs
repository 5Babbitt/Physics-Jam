using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperspace : MonoBehaviour
{
    public Color colour;
    public float colourValue;

    public float time;

    public Material material;
    public new Light light;

    private void Update() 
    {
        colourValue = (colourValue + Time.deltaTime / time) % 1.0f;
        
        colour = Color.HSVToRGB(colourValue, 1, 1);

        material.color = colour;
        material.SetColor("_EmissionColor", colour * 5);
        light.color = colour;
    }
}

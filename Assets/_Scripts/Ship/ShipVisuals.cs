using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVisuals : ShipSystem
{
    public Color bodyColour, lightColour;
    public Material bodyMat, emissiveMat;

    public GameObject shipModel;
    
    [SerializeField] private float intensity = 5f;

    protected override void Awake() 
    {
        base.Awake();   
        
        lightColour = ship.id.ShipColor;
        
        bodyMat = shipModel.GetComponent<MeshRenderer>().materials[0];
        emissiveMat = shipModel.GetComponent<MeshRenderer>().materials[1];
        
        bodyMat.color = bodyColour;
        emissiveMat.SetColor("_EmissionColor", lightColour * intensity);
    }

    private void OnValidate() 
    {
        bodyMat = shipModel.GetComponentInChildren<MeshRenderer>().sharedMaterials[0];
        emissiveMat = shipModel.GetComponentInChildren<MeshRenderer>().sharedMaterials[1];
        
        bodyMat.color = bodyColour;
        emissiveMat.SetColor("_EmissionColor", lightColour * intensity);
    }
}

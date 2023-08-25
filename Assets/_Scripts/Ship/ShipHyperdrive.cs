using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHyperdrive : ShipSystem
{
    
    [SerializeField] private float timeInHyperspace = 3f;

    public void TeleportToPosition(Vector3 position)
    {
        
    }

    public IEnumerator EnterHyperspace()
    {
        
        
        yield return new WaitForSeconds(timeInHyperspace);
    }
}

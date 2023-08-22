using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipSystem : MonoBehaviour
{
    protected Ship ship;

    protected virtual void Awake()
    {
        ship = transform.root.GetComponent<Ship>();
    }
}

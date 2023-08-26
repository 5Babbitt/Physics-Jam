using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible
{
    public void TakeDamage(int amount);
    public void Fracture();
}

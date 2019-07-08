using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstopableDamageDealer : DamageDealer
{
    public override void Hit()
    {
        Debug.Log("Hit");
    }
}

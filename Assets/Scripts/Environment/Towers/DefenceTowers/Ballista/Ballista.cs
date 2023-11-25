using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : DefenceTower
{
    public void Awake()
    {
        shootingCooldown = 1f;
        shellSpeed = 20f;
    }
}

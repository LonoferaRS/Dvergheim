using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : DefenceTower
{
    private void Awake()
    {
        shootingCooldown = 5f;
        shellSpeed = 40f;
    }
}

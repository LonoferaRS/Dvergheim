using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : DefenceTower
{
    public static float _cost { get; private set; } = 700f;
    private void Awake()
    {
        shootingCooldown = 5f;
        shellSpeed = 40f;
        cost = _cost;
    }
}

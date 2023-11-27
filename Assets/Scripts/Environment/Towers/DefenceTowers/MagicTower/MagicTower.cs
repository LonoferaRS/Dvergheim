using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : DefenceTower
{
    public static float _cost { get; private set; } = 500f;
    private void Awake()
    {
        shootingCooldown = 2f;
        shellSpeed = 15f;
        cost = _cost;
    }
}

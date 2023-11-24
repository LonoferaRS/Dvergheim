using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : DefenceTower
{
    private void Awake()
    {
        damage = 100f;
        shootingCooldown = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : DefenceTower
{
    public void Awake()
    {
        damage = 500;
        armorDecreaseConst = 0.5f;
        shootingCooldown = 5f;
    }
}

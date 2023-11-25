using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : DefenceTower
{
    public void Awake()
    {
        damage = 100f;
        armorDecreaseConst = 0.35f;
        shootingCooldown = 1f;
        shellSpeed = 20f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : DefenceTower
{
    public float splashHPConst { get; private set; } = 2f;
    public float splashArmorConst { get; private set; } = 4f;

    private void Awake()
    {
        damage = 500;
        armorDecreaseConst = 0.4f;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Ballista : DefenceTower
{
    public static float _cost { get; private set; } = 300f;
    private void Awake()
    {
        shootingCooldown = 1f;
        cost = _cost;
        shellSpeed = 20f;
    }
}

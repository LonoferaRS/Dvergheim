using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Tower
{
    public static float _cost { get; private set; } = 200f;

    private void Awake()
    {
        cost = _cost;
    }
}

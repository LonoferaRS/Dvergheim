using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShell : BaseShell
{
    private void Awake()
    {
        damage = 100f;
        armorDecreaseConst = 1;
        shellLifetime = 2f;
    }

    private void Start()
    {
        Destroy(gameObject, shellLifetime);
    }
}

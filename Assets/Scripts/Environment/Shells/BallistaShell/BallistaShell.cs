using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaShell : BaseShell
{
    private void Awake()
    {
        damage = 100f;
        armorDecreaseConst = 0.35f;
        shellLifetime = 1.5f;
    }





    private void Start()
    {
        Destroy(gameObject, shellLifetime);
    }
}

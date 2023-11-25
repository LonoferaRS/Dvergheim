using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mortar : DefenceTower
{
    public float splashHPConst { get; private set; } = 2f;
    public float splashArmorConst { get; private set; } = 4f;






    private void Awake()
    {
        damage = 500;
        armorDecreaseConst = 0.4f;
        shootingCooldown = 8f;
        shellSpeed = 8f;
    }





    protected override void Shoot()
    {
        base.Shoot();

        MortarShell mortarShell = currentShell.GetComponent<MortarShell>();

        // Устанавливаем позицию, на которой снаряд будет взорван
        mortarShell.Invoke("ExplodeShell", 2f);

    }
}

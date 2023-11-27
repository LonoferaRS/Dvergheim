using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Mortar : DefenceTower
{
    public static float _cost { get; private set; } = 600f;
    public float splashHPConst { get; private set; } = 2f;
    public float splashArmorConst { get; private set; } = 4f;






    private void Awake()
    {
        shootingCooldown = 8f;
        shellSpeed = 8f;
        cost = _cost;
    }





    protected override void Shoot()
    {
        base.Shoot();

        MortarShell mortarShell = currentShell.GetComponent<MortarShell>();

        // Устанавливаем позицию, на которой снаряд будет взорван
        mortarShell.Invoke("ExplodeShell", 2f);

    }
}

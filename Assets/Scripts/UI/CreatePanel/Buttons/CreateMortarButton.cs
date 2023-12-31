using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMortarButton : CreateButton
{
    public void OnButtonPressed()
    {
        if (residualHealth != 0)
        {
            wasPressed = true;
            TowerManager.instance.SetMortar();
        }
    }



    protected override void CountSubtractiveHP()
    {
        currentHealth = float.Parse(coinsCountText.text);
        residualHealth = currentHealth - Mortar._cost < 0 ? 0 : currentHealth - Mortar._cost;
    }
}

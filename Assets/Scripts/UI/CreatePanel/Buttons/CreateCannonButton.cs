using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCannonButton : CreateButton
{
    public void OnButtonPressed()
    {
        if (residualHealth != 0)
        {
            wasPressed = true;
            TowerManager.instance.SetCannon();
        }    
    }



    protected override void CountSubtractiveHP()
    {
        currentHealth = float.Parse(coinsCountText.text);
        residualHealth = currentHealth - Cannon._cost < 0 ? 0 : currentHealth - Cannon._cost;
    }
}

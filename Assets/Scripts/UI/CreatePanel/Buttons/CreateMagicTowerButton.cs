using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMagicTowerButton : CreateButton
{
    public void OnButtonPressed()
    {
        if (residualHealth != 0)
        {
            wasPressed = true;
            TowerManager.instance.SetCatapult();
        }
    }




    protected override void CountSubtractiveHP()
    {
        currentHealth = float.Parse(coinsCountText.text);
        residualHealth = currentHealth - MagicTower._cost < 0 ? 0 : currentHealth - MagicTower._cost;
    }
}

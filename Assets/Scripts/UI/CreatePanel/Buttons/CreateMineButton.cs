using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMineButton : CreateButton
{
    public void OnButtonPressed()
    {
        if (residualHealth != 0)
        {
            wasPressed = true;
            TowerManager.instance.SetMine();
        }
    }



    protected override void CountSubtractiveHP()
    {
        currentHealth = float.Parse(coinsCountText.text);
        residualHealth = currentHealth - Mine._cost < 0 ? 0 : currentHealth - Mine._cost;
    }
}

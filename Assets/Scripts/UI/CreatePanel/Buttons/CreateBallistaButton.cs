using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateBallistaButton : CreateButton
{
    public void OnButtonClick()
    {
        if (residualHealth != 0)
        {
            wasPressed = true;
            TowerManager.instance.SetBallista();
        }
    }



    protected override void CountSubtractiveHP()
    {
        currentHealth = float.Parse(coinsCountText.text);
        residualHealth = currentHealth - Ballista._cost < 0 ? 0 : currentHealth - Ballista._cost;
    }
}

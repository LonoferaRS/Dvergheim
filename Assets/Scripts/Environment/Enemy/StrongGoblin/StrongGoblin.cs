using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongGoblin : Goblin
{

    private void Awake()
    {
        healthPoints = 400f;
        armorPoints = 0f;
        damage = 100f;
        costForDeath = 60f;
    }

    public override bool HasArmor() { return false; }
}

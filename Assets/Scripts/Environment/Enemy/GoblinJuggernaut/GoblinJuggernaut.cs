using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinJuggernaut : ArmoredGoblin
{

    private void Awake()
    {
        healthPoints = 100f;
        armorPoints = 500f;
        damage = 500f;
    }
}

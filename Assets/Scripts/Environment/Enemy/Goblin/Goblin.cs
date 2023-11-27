using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin : Enemy
{
    [SerializeField] private Slider healthBarSlider;


    private void Awake()
    {
        healthPoints = 100f;
        armorPoints = 0f;
        damage = 50f;
    }


    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        base.TakeDamage(damage, armorDecreaseConst);

        // Меняем количество HP в HealthBar
        float currentHealthPercent = 100 * healthPoints / startHealth;
        healthBarSlider.value = currentHealthPercent / 100;
    }
}

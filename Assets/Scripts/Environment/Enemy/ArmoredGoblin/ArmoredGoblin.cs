using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoredGoblin : Enemy
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider armorBarSlider;


    private void Awake()
    {
        healthPoints = 100f;
        armorPoints = 100f;
    }


    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        base.TakeDamage(damage, armorDecreaseConst);

        // ������ ���������� HP � HealthBar
        float currentHealthPercent = 100 * healthPoints / startHealth;
        healthBarSlider.value = currentHealthPercent / 100;

        // ������ ���������� ����� � ArmorBar
        float currentArmorPetcent = 100 * armorPoints / startArmor;
        armorBarSlider.value = currentArmorPetcent / 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoredGoblin : Enemy
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider armorBarSlider;
    [SerializeField] private GameObject ArmorLosingPrefab;
    [SerializeField] private Sprite toGoblinSprite;
    private bool hasArmor = true;
    public string enemyTag = "Enemy";

    private void Awake()
    {
        healthPoints = 100f;
        armorPoints = 1000f;
        damage = 50f;
        costForDeath = 35f;
        moveSpeed = 0f;
    }

    public override bool HasArmor() { return true; }

    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        base.TakeDamage(damage, armorDecreaseConst);

        if (healthBarSlider != null) { 
            // Меняем количество HP в HealthBar
            float currentHealthPercent = 100 * healthPoints / startHealth;
            healthBarSlider.value = currentHealthPercent / 100;
        }

        if (armorBarSlider != null) { 
            // Меняем количество брони в ArmorBar
            float currentArmorPetcent = 100 * armorPoints / startArmor;
            armorBarSlider.value = currentArmorPetcent / 100;
        }

        if (armorPoints == 0 && hasArmor == true)
        {
            LosingArmor();
        }
    }

    private void LosingArmor()
    {
        // Получаем компонент SpriteRenderer текущего объекта
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Проверяем, что компонент SpriteRenderer существует
        if (spriteRenderer != null)
        {
            // Устанавливаем новый спрайт
            spriteRenderer.sprite = toGoblinSprite;
        }
        else
        {
            // Если компонент SpriteRenderer отсутствует, выводим сообщение об ошибке в консоль
            Debug.LogError("SpriteRenderer component not found on the object.");
        }
        GameObject ArmorLosingObject = Instantiate(ArmorLosingPrefab, transform.position, Quaternion.identity);
        hasArmor = false;
    }
    public override void ArmorHeal(float armorHealValue)
    {
        if (armorPoints > 0)
        {
            float armorAfterHeal = armorPoints + armorHealValue;
            if (armorAfterHeal > startArmor)
            {
                armorAfterHeal = startArmor;
            }
            armorPoints = armorAfterHeal;
        }
    }
}

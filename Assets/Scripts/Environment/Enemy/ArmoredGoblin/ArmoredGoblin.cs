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
            // ������ ���������� HP � HealthBar
            float currentHealthPercent = 100 * healthPoints / startHealth;
            healthBarSlider.value = currentHealthPercent / 100;
        }

        if (armorBarSlider != null) { 
            // ������ ���������� ����� � ArmorBar
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
        // �������� ��������� SpriteRenderer �������� �������
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // ���������, ��� ��������� SpriteRenderer ����������
        if (spriteRenderer != null)
        {
            // ������������� ����� ������
            spriteRenderer.sprite = toGoblinSprite;
        }
        else
        {
            // ���� ��������� SpriteRenderer �����������, ������� ��������� �� ������ � �������
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

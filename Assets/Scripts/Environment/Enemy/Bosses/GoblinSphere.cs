using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoblinBoss : Enemy
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] public GameObject goblinSwarmPrefab; 
    private bool isInsideTowerCollider = false;
    private void Awake()
    {
        healthPoints = 500f; // Примерное количество HP для босса
        armorPoints = 0f; // Примерное количество брони для босса
        costForDeath = 300f; // Примерное количество очков за уничтожение босса
       
        moveSpeed = 4f;
    }
    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        base.TakeDamage(damage, armorDecreaseConst);

        // Меняем количество HP в HealthBar
        if (healthBarSlider != null)
        {
            float currentHealthPercent = 100 * healthPoints / startHealth;
            healthBarSlider.value = currentHealthPercent / 100;

  
        }
    }
    public override void Heal(float healvalue)
    {
        base.Heal(healvalue);
        // Обновляем HealthBar
        if (healthBarSlider != null)
        {
            float currentHealthPercent = 100 * healthPoints / startHealth;
            healthBarSlider.value = currentHealthPercent / 100;

        }
    }

    public override void Die()
    {
        if (!isInsideTowerCollider) // Проверяем, находился ли объект внутри коллайдера башни перед смертью
        {
            mainTower.IncreaseHealth(costForDeath);
        }

        GameObject deathEffectObject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(goblinSwarmPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>(); // Получаем компонент Enemy с объекта
            if (enemy.startArmor == 0 )
            {
                Destroy(collision.gameObject);
                Heal(30);       // Количество здоровья от поглощения гоблинов
            }
        }

        if (collision.gameObject.CompareTag("MainTower"))
        {
            isInsideTowerCollider = true; // Устанавливаем флаг в true, когда объект входит в зону коллайдера башни
            MainTower mainTower = collision.gameObject.GetComponent<MainTower>();

            if (mainTower != null)
            {
                Die();
            }
            else { Debug.Log("Невозможно нанести урон башне, так как MainTower is null"); Destroy(gameObject); }
        }
    }
}

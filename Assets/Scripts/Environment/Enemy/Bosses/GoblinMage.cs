using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinMage : Enemy
{
    [SerializeField] private Slider healthBarSlider;

    public string enemyTag = "Enemy";
    public string towerTag = "DefenceTower";
    public float abilityRadius = 25f; // Радиус действия способности бессмертия
    public float TowerAbilityRadius = 10f; // Радиус действия способности дизактивации башен
    private const float TowerStopRadius = 8f;
    public float ArmorHealAbilityRadius = 3f; // Радиус способности восстановления брони
    public float abilityInterval = 10f; // Интервал между применением способности бессмертия
    public float TowerAbilityInterval = 8f; // Интервал между применением способности дизактивации башен
    public float SelfHealAbilityInterval = 15f; // Интервал между применением способности самолечения
    public float ArmorHealAbilityInterval = 10f; // Интервал между применением способности восстановления брони
    private void Awake()
    {
        healthPoints = 1500f;
        armorPoints = 0f;
        damage = 50f;
        costForDeath = 100f;
        moveSpeed = 2.5f;

        InvokeRepeating(nameof(ImmortalityAbility), 2f, abilityInterval);
        InvokeRepeating(nameof(DefenceTowerDisactivateAbility), 2f, TowerAbilityInterval);
        InvokeRepeating(nameof(SelfHealAbility), 5f, SelfHealAbilityInterval);
        InvokeRepeating(nameof(ArmorHealAbility), 1f, ArmorHealAbilityInterval);

    }

    private void OnDrawGizmosSelectedImmortality()
    {
        // Устанавливаем цвет гизмоны
        Gizmos.color = Color.red;
        // Рисуем сферу вокруг босса с радиусом abilityRadius
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
    }
    private void OnDrawGizmosSelectedTowersDisactivate()
    {
        // Устанавливаем цвет гизмоны
        Gizmos.color = Color.blue;
        // Рисуем сферу вокруг босса с радиусом abilityRadius
        Gizmos.DrawWireSphere(transform.position, TowerAbilityRadius);
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


    public override void Die()
    {
        base.Die();
    }

    private void ImmortalityAbility()
    {
        // Находим все объекты с заданным тегом в заданном радиусе от позиции босса
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            // Проходим по всем найденным объектам
            foreach (GameObject enemyObject in enemies)
            {
                // Проверяем расстояние до объекта
                if (Vector3.Distance(transform.position, enemyObject.transform.position) <= abilityRadius)
                {
                    // Получаем компонент Enemy и применяем способность
                    Enemy enemy = enemyObject.GetComponent<Enemy>();
                    if (enemy != null && enemy.startArmor == 0)
                    {
                        enemy.Immortality();
                    }
                }
            }
    }

    private void DefenceTowerDisactivateAbility()
    {
        // Находим все объекты с заданным тегом в заданном радиусе от позиции босса
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        foreach (GameObject towerobject in towers)
        {
            float distance = Vector3.Distance(transform.position, towerobject.transform.position);
            Debug.Log($"Distance = {distance}");
            Debug.Log($"Radius = {TowerStopRadius}");
            if (distance <= TowerStopRadius)
            {
                DefenceTower tower = towerobject.GetComponent<DefenceTower>(); // Получаем компонент башни
                if (tower != null)
                {
                    tower.DefenceTowerDisactivate(); // Выключаем башню
                }
            }
        }
    }

    private void SelfHealAbility()
    {
        if (healthPoints != startHealth)
        {
            Heal(800);
        }
    }

    private void ArmorHealAbility()
    {
        // Находим все объекты с заданным тегом в заданном радиусе от позиции босса
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject farthestArmoredGoblin = null;
        float maxDistance = 0f;

        // Проходим по всем найденным объектам
        foreach (GameObject enemyObject in enemies)
        {
            // Получаем компонент Enemy и проверяем тип
            ArmoredGoblin armoredGoblin = enemyObject.GetComponent<ArmoredGoblin>();
            if (armoredGoblin != null)
            {
                // Проверяем расстояние до объекта
                float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
                if (distance <= ArmorHealAbilityRadius && distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestArmoredGoblin = enemyObject;
                }
            }
        }

        if (farthestArmoredGoblin != null)
        {
            // Применяем лечение к самому дальнему бронированному юниту
            ArmoredGoblin armoredGoblin = farthestArmoredGoblin.GetComponent<ArmoredGoblin>();
            armoredGoblin.ArmorHeal(1000);
            Debug.Log("Лечение брони применено к самому дальнему бронированному юниту.");
        }
        else
        {
            Debug.Log("В радиусе действия способности нет бронированных юнитов.");
        }
    }

}


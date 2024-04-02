using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinMage : Enemy
{
    [SerializeField] private Slider healthBarSlider;

    public string enemyTag = "Enemy";
    public string towerTag = "DefenceTower";
    public float abilityRadius = 25f; // ������ �������� ����������� ����������
    public float TowerAbilityRadius = 10f; // ������ �������� ����������� ������������ �����
    private const float TowerStopRadius = 8f;
    public float ArmorHealAbilityRadius = 3f; // ������ ����������� �������������� �����
    public float abilityInterval = 10f; // �������� ����� ����������� ����������� ����������
    public float TowerAbilityInterval = 8f; // �������� ����� ����������� ����������� ������������ �����
    public float SelfHealAbilityInterval = 15f; // �������� ����� ����������� ����������� �����������
    public float ArmorHealAbilityInterval = 10f; // �������� ����� ����������� ����������� �������������� �����
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
        // ������������� ���� �������
        Gizmos.color = Color.red;
        // ������ ����� ������ ����� � �������� abilityRadius
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
    }
    private void OnDrawGizmosSelectedTowersDisactivate()
    {
        // ������������� ���� �������
        Gizmos.color = Color.blue;
        // ������ ����� ������ ����� � �������� abilityRadius
        Gizmos.DrawWireSphere(transform.position, TowerAbilityRadius);
    }


    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        base.TakeDamage(damage, armorDecreaseConst);

        // ������ ���������� HP � HealthBar
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
        // ������� ��� ������� � �������� ����� � �������� ������� �� ������� �����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            // �������� �� ���� ��������� ��������
            foreach (GameObject enemyObject in enemies)
            {
                // ��������� ���������� �� �������
                if (Vector3.Distance(transform.position, enemyObject.transform.position) <= abilityRadius)
                {
                    // �������� ��������� Enemy � ��������� �����������
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
        // ������� ��� ������� � �������� ����� � �������� ������� �� ������� �����
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        foreach (GameObject towerobject in towers)
        {
            float distance = Vector3.Distance(transform.position, towerobject.transform.position);
            Debug.Log($"Distance = {distance}");
            Debug.Log($"Radius = {TowerStopRadius}");
            if (distance <= TowerStopRadius)
            {
                DefenceTower tower = towerobject.GetComponent<DefenceTower>(); // �������� ��������� �����
                if (tower != null)
                {
                    tower.DefenceTowerDisactivate(); // ��������� �����
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
        // ������� ��� ������� � �������� ����� � �������� ������� �� ������� �����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject farthestArmoredGoblin = null;
        float maxDistance = 0f;

        // �������� �� ���� ��������� ��������
        foreach (GameObject enemyObject in enemies)
        {
            // �������� ��������� Enemy � ��������� ���
            ArmoredGoblin armoredGoblin = enemyObject.GetComponent<ArmoredGoblin>();
            if (armoredGoblin != null)
            {
                // ��������� ���������� �� �������
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
            // ��������� ������� � ������ �������� �������������� �����
            ArmoredGoblin armoredGoblin = farthestArmoredGoblin.GetComponent<ArmoredGoblin>();
            armoredGoblin.ArmorHeal(1000);
            Debug.Log("������� ����� ��������� � ������ �������� �������������� �����.");
        }
        else
        {
            Debug.Log("� ������� �������� ����������� ��� ������������� ������.");
        }
    }

}


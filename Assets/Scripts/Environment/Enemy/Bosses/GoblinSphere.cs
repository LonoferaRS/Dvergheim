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
        healthPoints = 500f; // ��������� ���������� HP ��� �����
        armorPoints = 0f; // ��������� ���������� ����� ��� �����
        costForDeath = 300f; // ��������� ���������� ����� �� ����������� �����
       
        moveSpeed = 4f;
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
    public override void Heal(float healvalue)
    {
        base.Heal(healvalue);
        // ��������� HealthBar
        if (healthBarSlider != null)
        {
            float currentHealthPercent = 100 * healthPoints / startHealth;
            healthBarSlider.value = currentHealthPercent / 100;

        }
    }

    public override void Die()
    {
        if (!isInsideTowerCollider) // ���������, ��������� �� ������ ������ ���������� ����� ����� �������
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
            Enemy enemy = collision.GetComponent<Enemy>(); // �������� ��������� Enemy � �������
            if (enemy.startArmor == 0 )
            {
                Destroy(collision.gameObject);
                Heal(30);       // ���������� �������� �� ���������� ��������
            }
        }

        if (collision.gameObject.CompareTag("MainTower"))
        {
            isInsideTowerCollider = true; // ������������� ���� � true, ����� ������ ������ � ���� ���������� �����
            MainTower mainTower = collision.gameObject.GetComponent<MainTower>();

            if (mainTower != null)
            {
                Die();
            }
            else { Debug.Log("���������� ������� ���� �����, ��� ��� MainTower is null"); Destroy(gameObject); }
        }
    }
}

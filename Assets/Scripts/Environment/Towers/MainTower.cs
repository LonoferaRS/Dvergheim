using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainTower : MonoBehaviour
{
    [SerializeField] private GameObject coinsCountObject;
    private TextMeshProUGUI coinsCountText;

    public float maxHealth = 1000;
    private float currentHealth;

    public HealthBar healthBar;

    public static bool IsGameOver = false;

    void Start()
    {
        coinsCountText = coinsCountObject.GetComponent<TextMeshProUGUI>();
        currentHealth = maxHealth;
        coinsCountText.text = currentHealth.ToString();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        // ��������� �������� �����
        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;

        // ��������� �����, ������������ ���������� �����
        coinsCountText.text = currentHealth.ToString();

        healthBar.SetHealth(currentHealth);

        // ���������, ���� �������� ����� ������ ��� ����� ����, ���������� ����� ��� ��������� ������ ��������
        if (currentHealth == 0)
        {
            GameOver();   // ����� ��� ���� �� = 0
        }
    }

    public void IncreaseHealth(float health)
    {
        // ���������� ���������� HP � �������� ����������
        currentHealth += health;

        // ��������� �����, ������������ ���������� �����
        coinsCountText.text = currentHealth.ToString();

        // ��������� HealthBar
        healthBar.SetHealth(currentHealth);
    }

    void GameOver()
    {
        IsGameOver = true;
    }






    void UpdateHealthUI()
    {
        // ��������� �������� �� ����
        healthBar.SetHealth(currentHealth);
    }
}



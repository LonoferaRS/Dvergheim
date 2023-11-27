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

    private bool isGameOver = false;

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

    void GameOver()
    {
        isGameOver = true;
        // ������������� ���� 
        Time.timeScale = 0f;
    }






    void UpdateHealthUI()
    {
        // ��������� �������� �� ����
        healthBar.SetHealth(currentHealth);
    }
}



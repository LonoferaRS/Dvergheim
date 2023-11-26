using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainTower : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public HealthBar healthBar;

    private bool isGameOver = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        // ��������� �������� �����
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        // ���������, ���� �������� ����� ������ ��� ����� ����, ���������� ����� ��� ��������� ������ ��������
        if (currentHealth <= 0)
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


    void OnTriggerEnter2D(Collider2D other)
    {
        // ��������, ��� � ������� ����� ������ � ����� "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // ���������� ������ �����
            Destroy(other.gameObject);
        }

        // ��������, ��� � ������� ����� ������ � ����� "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // ��������� �������� ����� �� 1
            TakeDamage(1);
        }
    }
}



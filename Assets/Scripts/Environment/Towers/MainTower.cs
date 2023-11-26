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
        // Уменьшаем здоровье башни
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        // Проверяем, если здоровье башни меньше или равно нулю, уничтожаем башню или выполняем другие действия
        if (currentHealth <= 0)
        {
            GameOver();   // Здесь код если хп = 0
        }
    }

    void GameOver()
    {
        isGameOver = true;
        // Останавливаем игру 
        Time.timeScale = 0f;
    }






    void UpdateHealthUI()
    {
        // Обновляем значение хп бара
        healthBar.SetHealth(currentHealth);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, что в триггер вошел объект с тегом "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Уничтожаем объект врага
            Destroy(other.gameObject);
        }

        // Проверка, что в триггер вошел объект с тегом "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Уменьшаем здоровье башни на 1
            TakeDamage(1);
        }
    }
}



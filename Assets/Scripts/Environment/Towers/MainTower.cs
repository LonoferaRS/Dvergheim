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
        // Уменьшаем здоровье башни
        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;

        // Обновляем текст, отображающий количество монет
        coinsCountText.text = currentHealth.ToString();

        healthBar.SetHealth(currentHealth);

        // Проверяем, если здоровье башни меньше или равно нулю, уничтожаем башню или выполняем другие действия
        if (currentHealth == 0)
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
}



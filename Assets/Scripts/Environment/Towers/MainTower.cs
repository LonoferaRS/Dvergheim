using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainTower : MonoBehaviour
{
    [SerializeField] private GameObject coinsCountObject;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private TextMeshProUGUI gameOverTextholder;
    private string gameOverText = "You lost!";

    private TextMeshProUGUI coinsCountText;

    public float maxHealth = 1000;
    private float currentHealth;

    public HealthBar healthBar;

    public bool IsGameOver { get; private set; } = false;

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
            GameOver(gameOverText);   // Здесь код если хп = 0
        }
    }

    public void IncreaseHealth(float health)
    {
        // Прибавляем полученное HP к текущему количеству
        currentHealth += health;

        // Обновляем текст, отображающий количество монет
        coinsCountText.text = currentHealth.ToString();

        // Обновляем HealthBar
        healthBar.SetHealth(currentHealth);
    }

    public void GameOver(string gameOverText)
    {
        IsGameOver = true;
        TowerManager.instance.isAnyPanelIsActive = true;

        // Активируем окно завершения игры и добавляем туда нужный текст
        gameOverWindow.SetActive(true);
        gameOverTextholder.text = gameOverText;

    }

    void UpdateHealthUI()
    {
        // Обновляем значение хп бара
        healthBar.SetHealth(currentHealth);
    }
}



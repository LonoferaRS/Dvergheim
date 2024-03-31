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
        // ��������� �������� �����
        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;

        // ��������� �����, ������������ ���������� �����
        coinsCountText.text = currentHealth.ToString();

        healthBar.SetHealth(currentHealth);

        // ���������, ���� �������� ����� ������ ��� ����� ����, ���������� ����� ��� ��������� ������ ��������
        if (currentHealth == 0)
        {
            GameOver(gameOverText);   // ����� ��� ���� �� = 0
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

    public void GameOver(string gameOverText)
    {
        IsGameOver = true;
        TowerManager.instance.isAnyPanelIsActive = true;

        // ���������� ���� ���������� ���� � ��������� ���� ������ �����
        gameOverWindow.SetActive(true);
        gameOverTextholder.text = gameOverText;

    }

    void UpdateHealthUI()
    {
        // ��������� �������� �� ����
        healthBar.SetHealth(currentHealth);
    }
}



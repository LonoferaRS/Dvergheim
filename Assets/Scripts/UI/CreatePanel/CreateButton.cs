using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected HealthBar healthBar;
    protected TextMeshProUGUI coinsCountText;

    protected float currentHealth;
    protected float residualHealth;

    private bool FirstEnabling = true;
    protected bool wasPressed;




    private void Start()
    {
        ButtonContainer buttonContainer = GetComponentInParent<ButtonContainer>();

        healthBar = buttonContainer.healthBar;
        coinsCountText = buttonContainer.coinsCountText;

        CountSubtractiveHP();
        FirstEnabling = false;
    }






    // ��������� ���� ���, ��� ��������� ������
    private void OnEnable()
    {
        wasPressed = false;

        if (!FirstEnabling)
        {
            CountSubtractiveHP();
        }
    }





    private void OnDisable()
    {
        if (!wasPressed) { ReturnHP(); }
    }






    protected virtual void CountSubtractiveHP() { }






    // ����������, ����� ������ ���� ������� �� ������
    public void OnPointerEnter(PointerEventData eventData) { SubtractHP(); }

    // ����������, ����� ������ ���� �� ������� �� ������
    public void OnPointerExit(PointerEventData eventData) { ReturnHP(); }






    private void SubtractHP()
    {
        // ���������� ������� HP ���������, ��� ������������ �����
        healthBar.SetHealth(residualHealth);
        coinsCountText.text = residualHealth.ToString();
    }






    private void ReturnHP()
    {
        // ���������� ���������� ���������� ���������� HP
        healthBar.SetHealth(currentHealth);
        coinsCountText.text = currentHealth.ToString();
    }
}

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






    // Выполняем этот код, при активации кнопки
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






    // Вызывается, когда курсор мыши наведен на кнопку
    public void OnPointerEnter(PointerEventData eventData) { SubtractHP(); }

    // Вызывается, когда курсор мыши не наведен на кнопку
    public void OnPointerExit(PointerEventData eventData) { ReturnHP(); }






    private void SubtractHP()
    {
        // Показываем сколько HP отнимится, при приобретении башни
        healthBar.SetHealth(residualHealth);
        coinsCountText.text = residualHealth.ToString();
    }






    private void ReturnHP()
    {
        // Прекращаем показывать отнимаемое количество HP
        healthBar.SetHealth(currentHealth);
        coinsCountText.text = currentHealth.ToString();
    }
}

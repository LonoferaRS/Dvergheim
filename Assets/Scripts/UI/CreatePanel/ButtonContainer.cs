using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonContainer : MonoBehaviour
{

    [SerializeField] private GameObject coinsCountObject;
    [SerializeField] private GameObject healthBarObject;
    
    public TextMeshProUGUI coinsCountText { get; private set; }
    public HealthBar healthBar { get; private set; }


    void Awake()
    {
        coinsCountText = coinsCountObject.GetComponent<TextMeshProUGUI>();
        healthBar = healthBarObject.GetComponent<HealthBar>();
    }
}

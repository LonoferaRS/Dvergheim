using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvasButton : MonoBehaviour
{
    [SerializeField] private GameObject canvas;


    public void OnButtonPressed()
    { 
        canvas.SetActive(false);
        TowerManager.instance.isAnyPanelIsActive = false;
    }
}

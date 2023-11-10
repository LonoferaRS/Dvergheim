using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMortarButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        TowerManager.instance.SetMortar();
    }
}

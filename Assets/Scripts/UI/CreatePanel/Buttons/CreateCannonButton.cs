using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCannonButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        TowerManager.instance.SetCannon();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCatapultButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        TowerManager.instance.SetCatapult();
    }
}

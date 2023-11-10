using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMineButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        TowerManager.instance.SetMine();
    }
}

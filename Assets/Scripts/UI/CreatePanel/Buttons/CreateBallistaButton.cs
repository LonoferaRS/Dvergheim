using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBallistaButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        TowerManager.instance.SetBallista();
    }
}

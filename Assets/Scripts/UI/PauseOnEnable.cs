using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEnable : MonoBehaviour
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
    }



    public void OnDisable()
    {
        Time.timeScale = 1f;
    }
}

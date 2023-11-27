using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float cost { get; protected set; }

    private void Start()
    {
        MainTower mainTower = GameObject.FindGameObjectWithTag("MainTower").GetComponent<MainTower>();

        mainTower.TakeDamage(cost);
    }
}

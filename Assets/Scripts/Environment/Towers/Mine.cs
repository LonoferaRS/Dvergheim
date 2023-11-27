using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Tower
{
    private MainTower mainTower;
    public static float _cost { get; private set; } = 200f;
    private float goldMiningRate = 50f;

    private void Awake()
    {
        cost = _cost;
    }

    public override void Start()
    {
        base.Start();
        mainTower = GameObject.FindGameObjectWithTag("MainTower").GetComponent<MainTower>();
        StartCoroutine(StartMining());
    }


    // Увеличивает HP башни каждые 30 сек на goldMiningRate
    private IEnumerator StartMining()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            mainTower.IncreaseHealth(goldMiningRate);
        }
    }
}

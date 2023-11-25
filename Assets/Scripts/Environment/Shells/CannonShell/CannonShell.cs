using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : BaseShell
{
    private void Awake()
    {
        damage = 500;
        armorDecreaseConst = 0.5f;
        shellLifetime = 1.5f;
    }

    private void Start()
    {
        Destroy(gameObject, shellLifetime);
    }





    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            //bool enemyHasBeenHit = false;

            if (enemy != null) { enemy.TakeDamage(damage, armorDecreaseConst); }
        }
    }
}

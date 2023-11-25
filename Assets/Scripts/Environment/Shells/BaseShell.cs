using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShell : MonoBehaviour
{
    public float damage { get; protected set; }
    public float armorDecreaseConst { get; protected set; }
    public float shellLifetime { get; protected set; } = 2f;


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null) { enemy.TakeDamage(damage, armorDecreaseConst); Destroy(gameObject); }
        }
    }
}

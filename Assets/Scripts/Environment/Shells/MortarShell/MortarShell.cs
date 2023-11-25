using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MortarShell : BaseShell
{
    [SerializeField] private Rigidbody2D shellRigidbody;
    [SerializeField] private CircleCollider2D shellCollider;
    [SerializeField] private SpriteRenderer shellSpriteRenderer;
    [SerializeField] private Sprite boomSprite;

    private float splashRadius = 5f;




    private void Awake()
    {
        damage = 500;
        armorDecreaseConst = 0.4f;
    }





    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Если столкновение с коллайдером врага
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ExplodeShell();
            Debug.Log("Коснулся! Снаряд должен был уничтожиться");
        }
    }





    private void ExplodeShell()
    {
        // Останавливаем снаряд
        shellRigidbody.velocity = Vector2.zero;

        // Меняю спрайт на спрайт взрыва (временная тема)
        shellSpriteRenderer.sprite = boomSprite;

        // Находим все объекты в радиусе взрыва с тегом "Enemy"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, splashRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Рассчитываем расстояние до объекта в радиусе взрыва
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                // Рассчитываем урон с учетом расстояния
                float splashDamage = CalculateSplashDamage(distance);

                // Применяем уменьшение урона по броне
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(splashDamage, armorDecreaseConst);
            }
        }

        // Удаляю снаряд через пару секунд
        Destroy(gameObject, 1.5f);
    }






    // Метод для расчета урона с учетом расстояния
    private float CalculateSplashDamage(float distance)
    {
        float maxDistance = splashRadius;
        float maxDamage = damage;
        float minDamage = 10;

        float splashDamage = Mathf.Lerp(maxDamage, minDamage, distance / maxDistance);

        return splashDamage;
    }
}

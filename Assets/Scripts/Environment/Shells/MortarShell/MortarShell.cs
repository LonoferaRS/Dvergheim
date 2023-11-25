using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MortarShell : MonoBehaviour
{
    [SerializeField] private Rigidbody2D shellRigidbody;
    [SerializeField] private CircleCollider2D shellCollider;
    [SerializeField] private SpriteRenderer shellSpriteRenderer;
    [SerializeField] private Sprite boomSprite;

    private Vector3 explosivePosition;
    public void SetExplosivePosition(Vector3 position) => explosivePosition = position;
    private float explosiveTime;
    public void SetExplosiveTime(float time) => explosiveTime = time;




    private void OnTriggerEnter2D(Collider2D collision)
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

        // Удаляю снаряд через пару секунд
        Destroy(gameObject, 1.5f);
    }
}

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
        // ���� ������������ � ����������� �����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ExplodeShell();
            Debug.Log("��������! ������ ������ ��� ������������");
        }
    }





    private void ExplodeShell()
    {
        // ������������� ������
        shellRigidbody.velocity = Vector2.zero;

        // ����� ������ �� ������ ������ (��������� ����)
        shellSpriteRenderer.sprite = boomSprite;

        // ������� ��� ������� � ������� ������ � ����� "Enemy"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, splashRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // ������������ ���������� �� ������� � ������� ������
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                // ������������ ���� � ������ ����������
                float splashDamage = CalculateSplashDamage(distance);

                // ��������� ���������� ����� �� �����
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(splashDamage, armorDecreaseConst);
            }
        }

        // ������ ������ ����� ���� ������
        Destroy(gameObject, 1.5f);
    }






    // ����� ��� ������� ����� � ������ ����������
    private float CalculateSplashDamage(float distance)
    {
        float maxDistance = splashRadius;
        float maxDamage = damage;
        float minDamage = 10;

        float splashDamage = Mathf.Lerp(maxDamage, minDamage, distance / maxDistance);

        return splashDamage;
    }
}

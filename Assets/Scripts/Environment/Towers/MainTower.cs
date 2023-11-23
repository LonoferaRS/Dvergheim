using UnityEngine;

public class MainTower : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, что в триггер вошел объект с тегом "Enemy"
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enter");
            // Уничтожаем объект врага
            Destroy(other.gameObject);
        }
    }
}

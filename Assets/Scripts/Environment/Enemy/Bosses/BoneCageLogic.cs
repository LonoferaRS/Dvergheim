using UnityEngine;

public class BoneShield : MonoBehaviour
{
    public Transform necromancer; // Ссылка на некроманта, за которым будет следовать щит
    public float followSpeed = 5f; // Скорость следования

    private void Update()
    {
        if (necromancer != null)
        {
            // Вычисляем направление к некроманту
            Vector3 direction = necromancer.position - transform.position;
            direction.y = 0f; // Игнорируем высоту, чтобы щит не двигался вверх и вниз

            // Вычисляем новую позицию для щита
            Vector3 newPosition = transform.position + direction.normalized * followSpeed * Time.deltaTime;

            // Перемещаем щит к новой позиции
            transform.position = newPosition;
        }
    }
}


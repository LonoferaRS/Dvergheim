using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenceTower : Tower
{

    private GameObject currentTarget;
    
    // Радиус обнаружения
    private float AimRadius = 7;

    // Скорость поворота башни
    private float rotationSpeed = 500f;







    private void FixedUpdate()
    {
        // Находим ближайший объект (в радиусе обнаружения) и наводимся на него
        FindNearestEnemy();
    }






    // Находит ближайшего врага в области видимости башни
    private void FindNearestEnemy()
    {
        if (currentTarget == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                (float distance, GameObject enemy) nearestEnemy = (float.MaxValue, null);

                foreach (GameObject enemy in enemies)
                {
                    float newDist = CountDistance(enemy);

                    if (newDist < AimRadius)
                    {
                        //Debug.Log($"Дистанция до цели ({newDist}) в радиусе обнаружения ({AimRadius})");
                        nearestEnemy = nearestEnemy.distance > newDist ? (newDist, enemy) : nearestEnemy;
                    }
                }

                currentTarget = nearestEnemy.enemy;

                //Debug.Log($"Имя ближайшего объекта = {currentTarget?.name ?? "null"}");
            }
        }
        else if (currentTarget != null)
        {
            AimOn(currentTarget);
        }
    }






    // Считает дистанцию до объекта, переданного в качестве параметра
    private float CountDistance(GameObject targetObject)
    {
        Vector2 towerPosition = transform.position;
        Vector2 enemyPosition = targetObject.transform.position;

        // Рассчитываю расстояние между башней и объектом
        float distance = Vector2.Distance(towerPosition, enemyPosition);

        return distance;
    }






    // Наводит ось Y башни на цель
    private void AimOn(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = targetPosition - transform.position;

        // Получаем угол наведения при помощи LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Поворачиваем башню
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}

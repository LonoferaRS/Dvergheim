using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenceTower : Tower
{

    private GameObject currentTarget;
    
    // ������ �����������
    private float AimRadius = 7;

    // �������� �������� �����
    private float rotationSpeed = 500f;







    private void FixedUpdate()
    {
        // ������� ��������� ������ (� ������� �����������) � ��������� �� ����
        FindNearestEnemy();
    }






    // ������� ���������� ����� � ������� ��������� �����
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
                        //Debug.Log($"��������� �� ���� ({newDist}) � ������� ����������� ({AimRadius})");
                        nearestEnemy = nearestEnemy.distance > newDist ? (newDist, enemy) : nearestEnemy;
                    }
                }

                currentTarget = nearestEnemy.enemy;

                //Debug.Log($"��� ���������� ������� = {currentTarget?.name ?? "null"}");
            }
        }
        else if (currentTarget != null)
        {
            AimOn(currentTarget);
        }
    }






    // ������� ��������� �� �������, ����������� � �������� ���������
    private float CountDistance(GameObject targetObject)
    {
        Vector2 towerPosition = transform.position;
        Vector2 enemyPosition = targetObject.transform.position;

        // ����������� ���������� ����� ������ � ��������
        float distance = Vector2.Distance(towerPosition, enemyPosition);

        return distance;
    }






    // ������� ��� Y ����� �� ����
    private void AimOn(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = targetPosition - transform.position;

        // �������� ���� ��������� ��� ������ LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // ������������ �����
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}

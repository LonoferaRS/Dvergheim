using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenceTower : Tower
{
    [SerializeField] protected GameObject shellPrefab;
    public float shootingCooldown { get; protected set; }

    public float shellSpeed { get; protected set; }


    private GameObject currentTarget;
    
    // ������ �����������
    private float AimRadius = 7;

    // �������� �������� �����
    private float rotationSpeed = 500f;

    // ����, ��� ������� �������, ��� ����� �������� �� ����
    private float aimingThreshold = 1f;

    private bool isShooting = false;
    
    public bool isDisactivated = false;

    [SerializeField] public SpriteRenderer magicSpriteRenderer;




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
            // ��������� �� ��������� ����
            AimOn(currentTarget);

            // ������� ��������� Enemy
            Enemy enemy = currentTarget.GetComponent<Enemy>();

            // ���� �������� - ��������
            if (IsAimed() && !isShooting && !enemy.invulable)
            {
                StartCoroutine(ShootCoroutine());
            }
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






    public Vector3 predicatedPosition { get; private set; }
    private Vector3 predicatedDirection;

    // ������� ��� Y ����� �� ����
    private void AimOn(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;

        // �������� �������� ����
        Enemy enemy = target.GetComponent<Enemy>();
        Vector2 targetVelocity = enemy != null ? enemy.velocity : Vector2.zero;

        // �������� �������������� �������
        predicatedPosition = PrefirePosition(targetPosition, targetVelocity);

        // �������� ����������� �� ��� ����
        predicatedDirection = (predicatedPosition - transform.position).normalized;

        // �������� ���� ��������� ��� ������ LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, predicatedDirection);

        // ������������ �����
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }






    // ���������� �������� �� ����� �� �����
    private bool IsAimed()
    {
        Vector3 targetPosition = currentTarget.transform.position;

        // �������� ���� ��������� ��� ������ LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, predicatedDirection);

        // ������������ ������� � �����
        float angleDifference = Quaternion.Angle(transform.rotation, lookRotation);

        // ���������� true, ���� ������� ������ ���������� ��������
        return angleDifference < aimingThreshold;
    }






    // �������� ����� Shoot � ������ �����������
    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        Shoot();

        yield return new WaitForSeconds(shootingCooldown);

        isShooting = false;
    }





    public GameObject currentShell { get; protected set; }
    protected virtual void Shoot()
    {
        if (!isDisactivated)
        {
            currentShell = Instantiate(shellPrefab, transform.position, transform.rotation);

            currentShell.GetComponent<Rigidbody2D>().velocity = predicatedDirection * shellSpeed;

            Debug.Log($"{name} is shooting");
        }
    }






    public float timeToTarget { get; private set; }

    // ������ �������������� ������� ������� � ������ ������� ������ ������� � ���������� �� ����
    private Vector3 PrefirePosition(Vector3 targetPosition, Vector2 targetVelocity)
    {

        timeToTarget = Vector3.Distance(transform.position, targetPosition) / shellSpeed;

        Vector3 predictedPosition = targetPosition + new Vector3(targetVelocity.x, targetVelocity.y, 0) * timeToTarget;

        return predictedPosition;
    }


    public virtual void DefenceTowerDisactivate()
    {
        isDisactivated = true;
        StartCoroutine(TemporaryDisactivate());
        if (magicSpriteRenderer != null)
        {
            magicSpriteRenderer.enabled = true;
        }
    }

    private IEnumerator TemporaryDisactivate()
    {
        float temp = rotationSpeed;
        rotationSpeed = 0;
        yield return new WaitForSeconds(5f); // ���� 5 ������
        // ����� 5 ������ ���������� ���������� �������� ��������
        isDisactivated = false;
        rotationSpeed = temp;

        if (magicSpriteRenderer != null)
        {
            magicSpriteRenderer.enabled = false;
        }
        Debug.LogError("Disactivate has ended.");
    }

}

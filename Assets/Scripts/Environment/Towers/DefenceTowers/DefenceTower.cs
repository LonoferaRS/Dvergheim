using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenceTower : Tower
{
    [SerializeField] private GameObject shellPrefab;
    public float damage { get; protected set; }
    public float armorDecreaseConst { get; protected set; }
    public float shootingCooldown { get; protected set; }
    public float shellSpeed { get; protected set; }
    public float shellLifetime { get; protected set; } = 2f;


    private GameObject currentTarget;
    
    // Радиус обнаружения
    private float AimRadius = 7;

    // Скорость поворота башни
    private float rotationSpeed = 500f;

    // Угол, при котором считаем, что башня наведена на цель
    private float aimingThreshold = 1f;

    private bool isShooting = false;
    







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
            // Наводимся на найденную цель
            AimOn(currentTarget);

            // Если навелись - стреляем
            if (IsAimed() && !isShooting)
            {
                StartCoroutine(ShootCoroutine());
            }
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






    public Vector3 predicatedPosition { get; private set; }
    private Vector3 predicatedDirection;

    // Наводит ось Y башни на цель
    private void AimOn(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;

        // Получаем скорость цели
        Enemy enemy = target.GetComponent<Enemy>();
        Vector2 targetVelocity = enemy != null ? enemy.velocity : Vector2.zero;

        // Получаем предполагаемую позицию
        predicatedPosition = PrefirePosition(targetPosition, targetVelocity);

        // Получаем направление на ход цели
        predicatedDirection = (predicatedPosition - transform.position).normalized;

        // Получаем угол наведения при помощи LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, predicatedDirection);

        // Поворачиваем башню
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }






    // Провереяет наведена ли башня на врага
    private bool IsAimed()
    {
        Vector3 targetPosition = currentTarget.transform.position;

        // Получаем угол наведения при помощи LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, predicatedDirection);

        // Рассчитываем разницу в углах
        float angleDifference = Quaternion.Angle(transform.rotation, lookRotation);

        // Возвращаем true, если разница меньше порогового значения
        return angleDifference < aimingThreshold;
    }






    // Вызывает метод Shoot с учетом перезарядки
    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        Shoot();

        yield return new WaitForSeconds(shootingCooldown);

        isShooting = false;
    }





    protected GameObject currentShell { get; private set; }
    protected virtual void Shoot()
    {
        currentShell = Instantiate(shellPrefab, transform.position, transform.rotation);

        currentShell.GetComponent<Rigidbody2D>().velocity = predicatedDirection * shellSpeed;

        Destroy(currentShell, shellLifetime);

        Debug.Log($"{name} is shooting");
    }






    public float timeToTarget { get; private set; }
    // Вернет предпологаемую позицию позицию с учетом времени полета снаряда и расстояния до цели
    private Vector3 PrefirePosition(Vector3 targetPosition, Vector2 targetVelocity)
    {
        timeToTarget = Vector3.Distance(transform.position, targetPosition) / shellSpeed;

        Vector3 predictedPosition = targetPosition + new Vector3(targetVelocity.x, targetVelocity.y, 0) * timeToTarget;

        return predictedPosition;
    }
}

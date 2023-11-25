using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    protected float healthPoints = 100f;
    protected float armorPoints = 0f;

    private Transform targetWaypoint;
    private List<Transform> visitedWaypoints = new List<Transform>();
    public Vector2 velocity { get; private set; }
    public float moveSpeed { get; protected set; } = 5f;



    void Start()
    {
        // Найти ближайшую путевую точку при старте
        FindNearestWaypoint();
    }

    void Update()
    {
        // Перемещение к текущей путевой точке
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (targetWaypoint == null)
        {
            // Если нет текущей путевой точки, просто вернуться
            return;
        }

        // Получаем вектор движения к точке
        Vector2 movementVector = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Получаю скорость
        velocity = (movementVector - (Vector2)transform.position) / Time.deltaTime;

        // Применяем вектор движения к точке
        transform.position = movementVector;

        // Если достигнута текущая путевая точка, добавить ее в список посещенных и найти следующую
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            visitedWaypoints.Add(targetWaypoint);
            FindNearestWaypoint();
        }
    }

    void FindNearestWaypoint()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // Проверка наличия путевых точек
        if (waypoints.Length == 0)
        {
            Debug.LogError("Отсутствуют путевые точки!");
            return;
        }

        // Найти ближайшую путевую точку, исключив посещенные точки
        float shortestDistance = Mathf.Infinity;
        Transform nearestWaypoint = null;

        foreach (GameObject waypointObject in waypoints)
        {
            Transform waypoint = waypointObject.transform;

            if (!visitedWaypoints.Contains(waypoint))
            {
                float distanceToWaypoint = Vector2.Distance(transform.position, waypoint.position);

                if (distanceToWaypoint < shortestDistance)
                {
                    shortestDistance = distanceToWaypoint;
                    nearestWaypoint = waypoint;
                }
            }
        }

        // Установить новую путевую точку
        targetWaypoint = nearestWaypoint;
    }
}

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
        // ����� ��������� ������� ����� ��� ������
        FindNearestWaypoint();
    }

    void Update()
    {
        // ����������� � ������� ������� �����
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (targetWaypoint == null)
        {
            // ���� ��� ������� ������� �����, ������ ���������
            return;
        }

        // �������� ������ �������� � �����
        Vector2 movementVector = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // ������� ��������
        velocity = (movementVector - (Vector2)transform.position) / Time.deltaTime;

        // ��������� ������ �������� � �����
        transform.position = movementVector;

        // ���� ���������� ������� ������� �����, �������� �� � ������ ���������� � ����� ���������
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            visitedWaypoints.Add(targetWaypoint);
            FindNearestWaypoint();
        }
    }

    void FindNearestWaypoint()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // �������� ������� ������� �����
        if (waypoints.Length == 0)
        {
            Debug.LogError("����������� ������� �����!");
            return;
        }

        // ����� ��������� ������� �����, �������� ���������� �����
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

        // ���������� ����� ������� �����
        targetWaypoint = nearestWaypoint;
    }
}

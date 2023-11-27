using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected float startHealth;
    protected float startArmor;
    public float healthPoints { get; protected set; } = 100f;
    public float armorPoints { get; protected set; } = 0f;
    public float damage { get; protected set; }

    private Transform targetWaypoint;
    private List<Transform> visitedWaypoints = new List<Transform>();
    public Vector2 velocity { get; private set; }
    [SerializeField] public float moveSpeed { get; protected set; } = 3f;

    public AudioClip deathSound; // ���� ������ �������
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ����� ��������� ������� ����� ��� ������
        FindNearestWaypoint();

        // ���������� ��������� ���������� HP
        startHealth = healthPoints;

        // ���������� ��������� ���������� �����
        startArmor = armorPoints;
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






    public virtual void TakeDamage(float damage, float armorDecreaseConst)
    {
        if (armorPoints > 0)
        {
            TakeDamageOnArmor(damage, armorDecreaseConst);
        }
        else
        {
            TakeDamageOnHealth(damage);
        }

        if (healthPoints == 0)
        {
            Die();
        }
    }


    // ����� ��� ��������� ������ �������
    void Die()
    {
        // ����������� ���� ������
        if (deathSound != null && audioSource != null)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
        }
        // �������������� �������� ��� ������ �������
        Destroy(gameObject);
    }



    private void TakeDamageOnArmor(float damage, float armorDecreaseConst)
    {
        float armorAfterDamage = armorPoints - damage * armorDecreaseConst;

        if (armorAfterDamage >= 0)
        {
            armorPoints -= damage * armorDecreaseConst;
        }
        else if (armorAfterDamage < 0)
        {
            armorPoints = 0;
            TakeDamageOnHealth(armorAfterDamage * -1);
        }
    }





    private void TakeDamageOnHealth(float damage)
    {
        float healthAfterDamage = healthPoints - damage;
        healthPoints = healthAfterDamage > 0 ? healthAfterDamage : 0;
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainTower"))
        {
            MainTower mainTower = collision.gameObject.GetComponent<MainTower>();


            if (mainTower != null)
            {
                mainTower.TakeDamage(damage);
                Destroy(gameObject);
            }
            else { Debug.Log("���������� ������� ���� �����, ��� ��� MainTower is null"); Destroy(gameObject); }
        }
    }
}

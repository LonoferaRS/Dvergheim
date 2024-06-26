using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float startHealth;
    public float startArmor;
    public float healthPoints { get; protected set; } = 100f;
    public float armorPoints { get; protected set; } = 0f;
    public float damage { get; protected set; }
    public float costForDeath { get; protected set; }
    public bool invulable { get; set; }

    private Transform targetWaypoint;
    private List<Transform> visitedWaypoints = new List<Transform>();
    public Vector2 velocity { get; private set; }
    [SerializeField] public float moveSpeed { get; protected set; } = 3f;
    [SerializeField] private GameObject statsBarHolder;

    public MainTower mainTower;

    private bool isAlive = true;
    public GameObject deathEffectPrefab;

    private bool wasTurned = false;
    public bool isInvulnerable = false;
    private Sprite enemySprite;

    void Start()
    {

        // ����� ��������� ������� ����� ��� ������
        FindNearestWaypoint();

        // ���������� ��������� ���������� HP
        startHealth = healthPoints;

        // ���������� ��������� ���������� �����
        startArmor = armorPoints;

        // �������� MainTower
        mainTower = GameObject.FindGameObjectWithTag("MainTower").GetComponent<MainTower>();

        // �������� ������ �����
        enemySprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if (isAlive)
        {
            // ����������� � ������� ������� �����
            MoveToWaypoint();
        }
    }


    public virtual bool HasArmor() { return false; }


    // ����� ������� ������������ ������ ����� �� ������ ��������
    private void TrunSprite(Vector2 turnDirection)
    {


        // �������� ���� ��������� ��� ������ LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, turnDirection);

        // ������� ���� ��� StatsBarHolder
        Quaternion statsHolderRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0,1,0));

        // ������������ �����
        transform.rotation = lookRotation;

        // ������������ StatsHolder � �������� �������, ����� StatsBar �� �������� ������ � ������
        statsBarHolder.transform.rotation = statsHolderRotation;
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

        if (!wasTurned)
        {
            TrunSprite(velocity);
            wasTurned = true;
        }

        // ���� ���������� ������� ������� �����, �������� �� � ������ ���������� � ����� ���������
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            visitedWaypoints.Add(targetWaypoint);
            FindNearestWaypoint();
            
            wasTurned = false;
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
    public virtual void Die()
    {

        GameObject deathEffectObject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // �������� ������� �� ������ Enemy � ���� HP
        mainTower.IncreaseHealth(costForDeath);

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

    public virtual void Heal(float healValue)
    {
        float healthAfterHeal = healthPoints + healValue;
        if (healthAfterHeal > startHealth)
        {
            healthAfterHeal = startHealth;
        }
        healthPoints = healthAfterHeal; // ������������� ����� �������� ��������
    }

    public virtual void ArmorHeal(float armorHealValue)
    {
        if (armorPoints > 0)
        {
            float armorAfterHeal = armorPoints + armorHealValue;
            if (armorAfterHeal > startArmor)
            {
                armorAfterHeal = startArmor;
            }
            armorPoints = armorAfterHeal;
        }
    }



    public virtual void OnTriggerEnter2D(Collider2D collision)
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

    public virtual void Immortality()
    {
        Debug.Log("Immortality given");
        isInvulnerable = true;
        StartCoroutine(TemporaryImmortality());
    }

    private IEnumerator TemporaryImmortality()
    {
        yield return new WaitForSeconds(5f); // ���� 5 ������

        // ����� 5 ������ ���������� ���������� �������� ��������
        isInvulnerable = false;
        Debug.Log("Immortality has ended.");
    }
}

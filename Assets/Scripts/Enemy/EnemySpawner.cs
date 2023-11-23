using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ ��� ������
    public Transform[] spawnPoints; // ������ ����� ������
    public float minSpawnTime = 5f; // ����������� ����� �� ���������� ������
    public float maxSpawnTime = 10f; // ������������ ����� �� ���������� ������

    void Start()
    {
        // �������� ����� ��� ������ ������� ����� ��������� �����
        Invoke("SpawnObject", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnObject()
    {
        // �������� ��������� ����� ������
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ������� ������ � ��������� ����� ������
        Instantiate(objectToSpawn, randomSpawnPoint.position, Quaternion.identity);

        // �������� ����� ��� ������ ������� ����� ��������� �����
        Invoke("SpawnObject", Random.Range(minSpawnTime, maxSpawnTime));
    }
}




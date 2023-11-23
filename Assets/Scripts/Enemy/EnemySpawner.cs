using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Объект для спавна
    public Transform[] spawnPoints; // Массив точек спавна
    public float minSpawnTime = 5f; // Минимальное время до следующего спавна
    public float maxSpawnTime = 10f; // Максимальное время до следующего спавна

    void Start()
    {
        // Вызываем метод для спавна объекта через случайное время
        Invoke("SpawnObject", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnObject()
    {
        // Выбираем случайную точку спавна
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Создаем объект в выбранной точке спавна
        Instantiate(objectToSpawn, randomSpawnPoint.position, Quaternion.identity);

        // Вызываем метод для спавна объекта через случайное время
        Invoke("SpawnObject", Random.Range(minSpawnTime, maxSpawnTime));
    }
}




using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Объект для спавна
    public Transform[] spawnPoints; // Массив точек спавна
    public float timeBeforeFirstWave = 5f; // Время до появления первой волны
    public float timeBetweenWaves = 10f; // Время между волнами
    public int initialWaveSize = 3; // Начальное количество врагов в первой волне
    public int maxAdditionalEnemies = 4; // Максимальное количество дополнительных врагов в следующих волнах
    public int minAdditionalEnemies = 1; // Минимальное количество дополнительных врагов в следующих волнах
    public int maxWaves = 10; // Максимальное количество волн
    private int currentWaveSize; // Текущее количество врагов в волне
    private int currentWaveNumber; // Текущий номер волны
    private bool isGameFinished = false; // Флаг, указывающий, завершена ли игра


    public AudioClip waveStartSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        currentWaveNumber = 1;
        // Ждем перед появлением первой волны
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        // Начинаем спаунить первую волну
        StartCoroutine(SpawnWave(initialWaveSize));
    }

    IEnumerator SpawnWave(int waveSize)
    {
        // Проигрываем звук начала волны
        if (waveStartSound != null)
        {
            audioSource.clip = waveStartSound;
            audioSource.Play();
        }

        for (int i = 0; i < waveSize; i++)
        {
            // Выбираем случайную точку спавна
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Создаем объект в выбранной точке спавна
            Instantiate(objectToSpawn, randomSpawnPoint.position, Quaternion.identity);

            // Ждем перед спауном следующего врага в текущей волне
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        // Увеличиваем номер текущей волны
        currentWaveNumber++;

        // Проверяем, достигнуто ли максимальное количество волн
        if (currentWaveNumber <= maxWaves)
        {
            // Ждем перед началом следующей волны
            yield return new WaitForSeconds(timeBetweenWaves);

            // Увеличиваем размер следующей волны
            currentWaveSize = waveSize + Random.Range(minAdditionalEnemies, maxAdditionalEnemies + 1);

            // Запускаем спаун следующей волны
            StartCoroutine(SpawnWave(currentWaveSize));
        }
        else
        {
            // Игра завершена
            isGameFinished = true;
        }
    }
}


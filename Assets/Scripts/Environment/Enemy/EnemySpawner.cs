using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Unity.VisualScripting;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private MainTower mainTower;
    private string gameOverText = "You win!";

    // Нужно почистить эту свалку, которая осталась после прошлой реализации
    public GameObject goblinPrefab;
    public GameObject armoredGoblinPrefab;
    public GameObject strongGoblinPrefab;
    public GameObject juggernautGoblinPrefab;
    public Transform[] spawnPoints; // Массив точек спавна
    public float timeBeforeFirstWave = 5f; // Время до появления первой волны
    public float timeBetweenWaves = 10f; // Время между волнами
    public float timeBeforeSpawn = 0.3f; // Время между спавном
    public int maxAdditionalEnemies = 4; // Максимальное количество дополнительных врагов в следующих волнах
    public int minAdditionalEnemies = 1; // Минимальное количество дополнительных врагов в следующих волнах
    public int maxWaves = 15; // Максимальное количество волн
    private int currentWaveSize; // Текущее количество врагов в волне
    private int currentWaveNumber; // Текущий номер волны
    public static bool IsGameFinished = false; // Флаг, указывающий, завершена ли игра

    
    public int goblinsCount = 100;
    public int armoredGoblinsCount = 50;
    public int strongGoblinsCount = 80;
    public int juggernautsCount = 5;
    public GameObject bossPrefab;
    public int bossCount = 100; // количество боссов

    public AudioClip waveStartSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Ждем перед появлением первой волны
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        // Задаю параметры влияющие на волны гоблинов
        int waveCount = 8;
        double waveIncPercent = 1.4;
        double complexityParam = 1; // Не реализован
        

        // Начинаем спаунить первую волну
        StartCoroutine(StartSpawner(bossPrefab, bossCount ,goblinsCount, armoredGoblinsCount, strongGoblinsCount, juggernautsCount, waveCount, waveIncPercent, complexityParam));
    }


    void SetEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab != null) { 
            Transform randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }



    private float enemyCheckDelay = 5f; // Периодичность (в секундах) проверки присутствия гоблинов на уровне
    IEnumerator StartSpawner(GameObject bossPrefab,int bossCount, int GoblinsCount, int ArmoredGoblinsCount, int StrongGoblinsCount, int GoblinJuggernautsCount, int WavesCount, double waveIncPercent, double ComplexityParam) {

        double startGoblinsWave = CountFirstUnitCount(GoblinsCount, waveIncPercent, WavesCount);
        double startArmoredGoblinsWave = CountFirstUnitCount(ArmoredGoblinsCount, waveIncPercent, WavesCount);
        double startStrongGoblinsWave = CountFirstUnitCount(StrongGoblinsCount, waveIncPercent, WavesCount);
        double startJuggernautGoblinsWave = CountFirstUnitCount(GoblinJuggernautsCount, waveIncPercent, WavesCount);
        double startBossWave= CountFirstUnitCount(bossCount, waveIncPercent, WavesCount);   



        for (int i = 0; i < WavesCount; i++) 
        {

            PlayWaveStartSound(); // Проигрываем звук знаменующий начало волны

            // Спавним гоблинов
            SpawnEnemy(ref startGoblinsWave, waveIncPercent, i, goblinPrefab);
            SpawnEnemy(ref startStrongGoblinsWave, waveIncPercent, i, strongGoblinPrefab);
            SpawnEnemy(ref startArmoredGoblinsWave, waveIncPercent, i, armoredGoblinPrefab);
            SpawnEnemy(ref startJuggernautGoblinsWave, waveIncPercent, i, juggernautGoblinPrefab);

            if (i == 1) 
                {
                    SpawnEnemy(ref startBossWave, waveIncPercent, i , bossPrefab);
                    StartCoroutine(StartSpawner(bossPrefab, bossCount, 0, 0, 0, 0, 1, 1, 1));
                }


            // Пока гоблины присутствуют на уровне - ждем
            bool enemyIsAlive = true;

            while (enemyIsAlive) {

                // Ждем с заданной переодичностью
                yield return new WaitForSeconds(enemyCheckDelay);

                // Проверяем присутствие гоблинов на уровне
                enemyIsAlive = EnemyExists();
            }

            // Ждем перед началом новой волны
            yield return new WaitForSeconds(timeBetweenWaves);
        }


        Debug.Log($"Значение startGoblinsWave = {Math.Round(startGoblinsWave)}");

        // Игра завершена победой игрока
        IsGameFinished = true;
        mainTower.GameOver(gameOverText);

    }


    void SpawnEnemy(ref double waveCount, double incPercent, int waveIter, GameObject enemyPrefab) {
        
        if (waveIter > 0)
        {
            waveCount *= incPercent;
        }

        int spawnCount = (int) Math.Round(waveCount);

        StartCoroutine(SpawnEnemies(enemyPrefab, spawnCount));
    }



    IEnumerator SpawnEnemies(GameObject enemyPrefab, int spawnCount) {

        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(timeBeforeSpawn);
            SetEnemy(enemyPrefab);
        }
    }



    double CountFirstUnitCount(double totalNumber, double incParam, int waves) {

        return totalNumber / ((1 - Math.Pow(incParam, waves)) / (1 - incParam));
    }



    bool EnemyExists() {

        return GameObject.FindGameObjectsWithTag("Enemy").Count() > 0;
    }



    void PlayWaveStartSound() {

        if (waveStartSound != null)
        {
            audioSource.clip = waveStartSound;
            audioSource.Play();
        }
    }
}
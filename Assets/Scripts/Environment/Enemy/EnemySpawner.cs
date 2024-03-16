using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Unity.VisualScripting;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private MainTower mainTower;
    private string gameOverText = "You win!";

    public GameObject goblinPrefab;
    public GameObject armoredGoblinPrefab;
    public GameObject strongGoblinPrefab;
    public GameObject juggernautGoblinPrefab;
    public Transform[] spawnPoints; // ������ ����� ������
    public float timeBeforeFirstWave = 5f; // ����� �� ��������� ������ �����
    public float timeBetweenWaves = 10f; // ����� ����� �������
    public float timeBeforeSpawn = 0.3f; // ����� ����� �������
    public int maxAdditionalEnemies = 4; // ������������ ���������� �������������� ������ � ��������� ������
    public int minAdditionalEnemies = 1; // ����������� ���������� �������������� ������ � ��������� ������
    public int maxWaves = 15; // ������������ ���������� ����
    private int currentWaveSize; // ������� ���������� ������ � �����
    private int currentWaveNumber; // ������� ����� �����
    public static bool IsGameFinished = false; // ����, �����������, ��������� �� ����

    public AudioClip waveStartSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // ���� ����� ���������� ������ �����
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        // ����� ����� ���������� ��������
        int goblinsCount = 100;
        int armoredGoblinsCount = 50;
        int strongGoblinsCount = 80;
        int juggernautsCount = 5;

        // ����� ��������� �������� �� ����� ��������
        int waveCount = 8;
        double waveIncPercent = 1.4;
        double complexityParam = 1; // �� ����������
        

        // �������� �������� ������ �����
        StartCoroutine(StartSpawner(goblinsCount, armoredGoblinsCount, strongGoblinsCount, juggernautsCount, waveCount, waveIncPercent, complexityParam));
    }


    void SetEnemy(GameObject enemyPrefab)
    {
        Transform randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
    }



    private float enemyCheckDelay = 5f; // ������������� (� ��������) �������� ����������� �������� �� ������
    IEnumerator StartSpawner(int GoblinsCount, int ArmoredGoblinsCount, int StrongGoblinsCount, int GoblinJuggernautsCount, int WavesCount, double waveIncPercent, double ComplexityParam) {

        double startGoblinsWave = CountFirstUnitCount(GoblinsCount, waveIncPercent, WavesCount);
        double startArmoredGoblinsWave = CountFirstUnitCount(ArmoredGoblinsCount, waveIncPercent, WavesCount);
        double startStrongGoblinsWave = CountFirstUnitCount(StrongGoblinsCount, waveIncPercent, WavesCount);
        double startJuggernautGoblinsWave = CountFirstUnitCount(GoblinJuggernautsCount, waveIncPercent, WavesCount);



        for (int i = 0; i < WavesCount; i++) {

            PlayWaveStartSound(); // ����������� ���� ����������� ������ �����

            // ������� ��������
            SpawnEnemy(ref startGoblinsWave, waveIncPercent, i, goblinPrefab);
            SpawnEnemy(ref startStrongGoblinsWave, waveIncPercent, i, strongGoblinPrefab);
            SpawnEnemy(ref startArmoredGoblinsWave, waveIncPercent, i, armoredGoblinPrefab);
            SpawnEnemy(ref startJuggernautGoblinsWave, waveIncPercent, i, juggernautGoblinPrefab);


            // ���� ������� ������������ �� ������ - ����
            bool enemyIsAlive = true;

            while (enemyIsAlive) {

                // ���� � �������� ��������������
                yield return new WaitForSeconds(enemyCheckDelay);

                // ��������� ����������� �������� �� ������
                enemyIsAlive = EnemyExists();
            }

            // ���� ����� ������� ����� �����
            yield return new WaitForSeconds(timeBetweenWaves);
        }


        Debug.Log($"�������� startGoblinsWave = {Math.Round(startGoblinsWave)}");

        // ���� ��������� ������� ������
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
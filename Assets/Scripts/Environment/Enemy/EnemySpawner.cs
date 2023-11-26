using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ ��� ������
    public Transform[] spawnPoints; // ������ ����� ������
    public float timeBeforeFirstWave = 5f; // ����� �� ��������� ������ �����
    public float timeBetweenWaves = 10f; // ����� ����� �������
    public int initialWaveSize = 3; // ��������� ���������� ������ � ������ �����
    public int maxAdditionalEnemies = 4; // ������������ ���������� �������������� ������ � ��������� ������
    public int minAdditionalEnemies = 1; // ����������� ���������� �������������� ������ � ��������� ������
    public int maxWaves = 10; // ������������ ���������� ����
    private int currentWaveSize; // ������� ���������� ������ � �����
    private int currentWaveNumber; // ������� ����� �����
    private bool isGameFinished = false; // ����, �����������, ��������� �� ����


    public AudioClip waveStartSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        currentWaveNumber = 1;
        // ���� ����� ���������� ������ �����
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        // �������� �������� ������ �����
        StartCoroutine(SpawnWave(initialWaveSize));
    }

    IEnumerator SpawnWave(int waveSize)
    {
        // ����������� ���� ������ �����
        if (waveStartSound != null)
        {
            audioSource.clip = waveStartSound;
            audioSource.Play();
        }

        for (int i = 0; i < waveSize; i++)
        {
            // �������� ��������� ����� ������
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // ������� ������ � ��������� ����� ������
            Instantiate(objectToSpawn, randomSpawnPoint.position, Quaternion.identity);

            // ���� ����� ������� ���������� ����� � ������� �����
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        // ����������� ����� ������� �����
        currentWaveNumber++;

        // ���������, ���������� �� ������������ ���������� ����
        if (currentWaveNumber <= maxWaves)
        {
            // ���� ����� ������� ��������� �����
            yield return new WaitForSeconds(timeBetweenWaves);

            // ����������� ������ ��������� �����
            currentWaveSize = waveSize + Random.Range(minAdditionalEnemies, maxAdditionalEnemies + 1);

            // ��������� ����� ��������� �����
            StartCoroutine(SpawnWave(currentWaveSize));
        }
        else
        {
            // ���� ���������
            isGameFinished = true;
        }
    }
}


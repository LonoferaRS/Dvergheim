using UnityEngine;
using System.Collections;

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
    public float timeBeforeSpawn = 10f; // ����� ����� �������
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
        currentWaveNumber = 0;
        // ���� ����� ���������� ������ �����
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        // �������� �������� ������ �����
        StartCoroutine(SpawnWave());
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
    }

    IEnumerator SpawnWave()
    {
        // ����������� ���� ������ �����
        if (waveStartSound != null)
        {
            audioSource.clip = waveStartSound;
            audioSource.Play();
        }

        // ����������� ����� ������� �����
        currentWaveNumber++;

        // ���������, ���������� �� ������������ ���������� ����
        if (currentWaveNumber <= maxWaves)
        {
            switch (currentWaveNumber)
            {
                case 1:
                    SpawnGoblins(3);
                    break;
                case 2:
                    SpawnGoblins(6);
                    break;
                case 3:
                    SpawnGoblins(8);
                    SpawnArmoredGoblins(1);
                    break;
                case 4:
                    SpawnGoblins(10);
                    SpawnArmoredGoblins(1);
                    break;
                case 5:
                    SpawnGoblins(10);
                    SpawnArmoredGoblins(2);
                    break;
                case 6:
                    SpawnGoblins(12);
                    SpawnArmoredGoblins(2);
                    break;
                case 7:
                    SpawnGoblins(10);
                    SpawnArmoredGoblins(2);
                    SpawnStrongGoblins(1);
                    break;
                case 8:
                    SpawnGoblins(13);
                    SpawnArmoredGoblins(3);
                    SpawnStrongGoblins(1);
                    break;
                case 9:
                    SpawnGoblins(13);
                    SpawnArmoredGoblins(3);
                    SpawnStrongGoblins(2);
                    break;
                case 10:
                    SpawnGoblins(15);
                    SpawnArmoredGoblins(4);
                    SpawnStrongGoblins(2);
                    break;
                case 11:
                    SpawnGoblins(17);
                    SpawnArmoredGoblins(4);
                    SpawnStrongGoblins(2);
                    break;
                case 12:
                    SpawnGoblins(17);
                    SpawnArmoredGoblins(5);
                    SpawnStrongGoblins(2);
                    SpawnJaggernauts(1);
                    break;
                case 13:
                    SpawnGoblins(20);
                    SpawnArmoredGoblins(5);
                    SpawnStrongGoblins(2);
                    SpawnJaggernauts(1);
                    break;
                case 14:
                    SpawnGoblins(20);
                    SpawnArmoredGoblins(7);
                    SpawnStrongGoblins(3);
                    SpawnJaggernauts(1);
                    break;
                case 15:
                    SpawnGoblins(20);
                    SpawnArmoredGoblins(7);
                    SpawnStrongGoblins(3);
                    SpawnJaggernauts(2);
                    break;
            }
            // ���� ����� ������� ��������� �����
            yield return new WaitForSeconds(timeBetweenWaves);

            // ��������� ����� ��������� �����
            StartCoroutine(SpawnWave());
        }
        else
        {
            // ���� ���������
            IsGameFinished = true;
            mainTower.GameOver(gameOverText);

        }
    }

    void SpawnGoblins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(goblinPrefab);
        }
    }

    void SpawnArmoredGoblins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(armoredGoblinPrefab);
        }
    }

    void SpawnStrongGoblins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(strongGoblinPrefab);
        }
    }

    void SpawnJaggernauts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(juggernautGoblinPrefab); 
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
        StartCoroutine(DelayedSpawn());
    }
}




//using UnityEngine;
//using System.Collections;

//public class ObjectSpawner : MonoBehaviour
//{
//    public GameObject goblinPrefab;
//    public GameObject armoredGoblinPrefab;
//    public GameObject strongGoblinPrefab;
//    public GameObject juggernautGoblinPrefab;

//    public Transform[] spawnPoints;
//    public float timeBeforeFirstWave = 5f;
//    public float timeBetweenWaves = 10f;
//    public float minSpawnDelay = 0.1f; // ����������� �������� ����� ������� ��������
//    public int maxWaves = 15;

//    private int currentWaveNumber;
//    public static bool IsGameFinished = false;

//    private AudioSource audioSource;
//    public AudioClip waveStartSound;

//    void Start()
//    {
//        audioSource = gameObject.AddComponent<AudioSource>();
//        currentWaveNumber = 1;
//        StartCoroutine(DelayedStart());
//    }

//    IEnumerator DelayedStart()
//    {
//        yield return new WaitForSeconds(timeBeforeFirstWave);
//        SpawnGoblins(3); // �� ������ ����� �������� 3 �������
//    }

//    IEnumerator SpawnWave(int goblinCount, int armoredCount, int strongCount, int juggernautCount)
//    {
//        PlayWaveStartSound();

//        yield return new WaitForSeconds(minSpawnDelay); // ����������� �������� ����� ������� ������

//        SpawnGoblins(goblinCount);
//        yield return new WaitForSeconds(minSpawnDelay); // ����������� �������� ����� ������ ������

//        SpawnArmoredGoblins(armoredCount);
//        yield return new WaitForSeconds(minSpawnDelay);

//        SpawnStrongGoblins(strongCount);
//        yield return new WaitForSeconds(minSpawnDelay);

//        SpawnJuggernautGoblins(juggernautCount);

//        yield return new WaitForSeconds(timeBetweenWaves);

//        currentWaveNumber++;

//        if (currentWaveNumber <= maxWaves)
//        {
//            int nextGoblinCount = goblinCount + (currentWaveNumber <= 5 ? 3 : 2);
//            int nextArmoredCount = armoredCount + (currentWaveNumber % 2 == 0 ? 1 : 0);
//            int nextStrongCount = strongCount + (currentWaveNumber % 3 == 0 ? 1 : 0);
//            int nextJuggernautCount = juggernautCount + (currentWaveNumber % 6 == 0 ? 1 : 0);

//            StartCoroutine(SpawnWave(nextGoblinCount, nextArmoredCount, nextStrongCount, nextJuggernautCount));
//        }
//        else
//        {
//            IsGameFinished = true;
//        }
//    }

//    void PlayWaveStartSound()
//    {
//        if (waveStartSound != null)
//        {
//            audioSource.clip = waveStartSound;
//            audioSource.Play();
//        }
//    }

//    void SpawnGoblins(int count)
//    {
//        for (int i = 0; i < count; i++)
//        {
//            SpawnEnemy(goblinPrefab);
//            yield return new WaitForSeconds(minSpawnDelay);
//        }
//    }

//    void SpawnArmoredGoblins(int count)
//    {
//        for (int i = 0; i < count; i++)
//        {
//            SpawnEnemy(armoredGoblinPrefab);
//            yield return new WaitForSeconds(minSpawnDelay);
//        }
//    }

//    void SpawnStrongGoblins(int count)
//    {
//        for (int i = 0; i < count; i++)
//        {
//            SpawnEnemy(strongGoblinPrefab);
//            yield return new WaitForSeconds(minSpawnDelay);
//        }
//    }

//    void SpawnJuggernautGoblins(int count)
//    {
//        for (int i = 0; i < count; i++)
//        {
//            SpawnEnemy(juggernautGoblinPrefab);
//            yield return new WaitForSeconds(minSpawnDelay);
//        }
//    }

//    void SpawnEnemy(GameObject enemyPrefab)
//    {
//        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
//        Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
//    }
//}
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;


    private Dictionary<Vector3Int, GameObject> towers = new Dictionary<Vector3Int, GameObject>();
    private List<GameObject> panels = new List<GameObject>();

    private Tilemap grassTilemap;

    private Vector3Int currentTilePosition;
    [SerializeField] private GameObject createPanel;
    [SerializeField] private GameObject ballistaPrefab;
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject mortarPrefab;
    [SerializeField] private GameObject catapultPrefab;
    [SerializeField] private GameObject minePrefab;

    public static bool isAnyPanelIsActive = false;

    //
    public AudioClip[] soundClips; // Массив звуков
    private AudioSource audioSource;
    //

    void Start()
    {
        audioSource = GetComponent<AudioSource>();



        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        grassTilemap = GetGrassTilemap();

        panels.Add(createPanel);
    }





    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isAnyPanelIsActive)
        {
            foreach (GameObject panel in panels)
            { 
                panel.SetActive(false);
            }

            Time.timeScale = 1.0f;
            isAnyPanelIsActive = false;
        }
    }





    private Tilemap GetGrassTilemap()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects) 
        {
            if (obj.GetComponent<TileDetection>() != null)
            {
                return obj.GetComponent<Tilemap>();
            }
        }
        return null;
    }





    public void ReciveTilePosition(Vector3Int tilePosition)
    {
        if (!isAnyPanelIsActive)
        {
            if (towers.ContainsKey(tilePosition))
            {

                Debug.Log("Это место занято другой постройкой");
                PlaySound(0);
            }
            else
            {
                currentTilePosition = tilePosition;
                ShowCreatePanel();
            }
        }
    }





    private void ShowCreatePanel()
    {
        createPanel.gameObject.SetActive(true);
        isAnyPanelIsActive = true;
    }





    private void HideCreatePanel()
    {
        createPanel.gameObject.SetActive(false);
        isAnyPanelIsActive = false;
    }





    public void SetTower(GameObject prefab)
    {
        if (prefab != null)
        {
            // Инстанцирую башню
            GameObject tower = Instantiate(prefab);

            // Устанавливаю позицию башне
            tower.transform.position = GetCenterTilePositionInWorld(currentTilePosition);

            // Добавляю в словарь
            towers[currentTilePosition] = tower;
            PlayRandomBuildSound();
        }
        else { Debug.Log("Не удалось установить башню, так как prefab is null"); }

        HideCreatePanel();
    }
    public void SetBallista() => SetTower(ballistaPrefab);
    public void SetCannon() => SetTower(cannonPrefab);
    public void SetMortar() => SetTower(mortarPrefab);
    public void SetCatapult() => SetTower(catapultPrefab);
    public void SetMine() => SetTower(minePrefab);










    private Vector3 GetCenterTilePositionInWorld(Vector3Int tilePosition)
    {
        return grassTilemap.CellToWorld(tilePosition) + grassTilemap.cellSize * 0.5f;
    }



    // Метод для проигрывания конкретного звука по индексу в массиве
    void PlaySound(int soundIndex)
    {
        // Проверяем, чтобы индекс не выходил за пределы массива
        if (soundIndex >= 0 && soundIndex < soundClips.Length)
        {
            // Устанавливаем выбранный звук в Audio Source
            audioSource.clip = soundClips[soundIndex];

            // Проигрываем звук
            audioSource.Play();
        }
        else
        {
            // Выводим предупреждение в консоль, если индекс некорректен
            Debug.LogWarning("Invalid sound index.");
        }
    }

    void PlayRandomBuildSound()
    {
        // Генерируем случайный индекс
        int randomIndex = Random.Range(1, soundClips.Length);

        // Устанавливаем выбранный звук в Audio Source
        audioSource.clip = soundClips[randomIndex];

        // Проигрываем звук
        audioSource.Play();
    }
}












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
    public Tilemap roadsTilemap;

    private Vector3Int currentTilePosition;
    [SerializeField] private GameObject createPanel;

    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject GameFinishedPanel;

    [SerializeField] private GameObject ballistaPrefab;
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject mortarPrefab;
    [SerializeField] private GameObject catapultPrefab;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private MainTower mainTower;

    public bool isAnyPanelIsActive { get; set; } = false;

    //
    public AudioClip[] soundClips;
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

        grassTilemap = GetGrassTilemap();

        panels.Add(createPanel);
        panels.Add(exitPanel);
    }





    void Update()
    {
        if (!mainTower.IsGameOver)
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
            else if (Input.GetKeyDown(KeyCode.Escape) && !isAnyPanelIsActive)
            {
                exitPanel.SetActive(true);
                isAnyPanelIsActive = true;
            }

            if (Input.GetMouseButtonDown(0) && !isAnyPanelIsActive)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = roadsTilemap.WorldToCell(mousePos);
                TileBase clickedTile = roadsTilemap.GetTile(cellPosition);
                // Проверка, что тайл существует и что он принадлежит Tilemap с дорогами
                if (clickedTile != null && clickedTile == roadsTilemap.GetTile(cellPosition))
                {
                    // Проигрывание звука
                    PlaySound(0);
                }
            }
        }
        else
        {
            Debug.Log("Игра считается завершенной");
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
                PlaySound(0);
                Debug.Log("Это место занято другой постройкой");
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

    void PlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundClips.Length)
        {
            audioSource.clip = soundClips[soundIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound index.");
        }
    }

    void PlayRandomBuildSound()
    {
        int randomIndex = Random.Range(1, soundClips.Length);
        audioSource.clip = soundClips[randomIndex];
        audioSource.Play();
    }
}
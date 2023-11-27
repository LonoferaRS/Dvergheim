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
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject ballistaPrefab;
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject mortarPrefab;
    [SerializeField] private GameObject catapultPrefab;
    [SerializeField] private GameObject minePrefab;


    public static bool isAnyPanelIsActive = false;


    //
    public AudioClip[] soundClips; // Ìàññèâ çâóêîâ
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

                Debug.Log("Ýòî ìåñòî çàíÿòî äðóãîé ïîñòðîéêîé");
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
            // Èíñòàíöèðóþ áàøíþ
            GameObject tower = Instantiate(prefab);

            // Óñòàíàâëèâàþ ïîçèöèþ áàøíå
            tower.transform.position = GetCenterTilePositionInWorld(currentTilePosition);

            // Äîáàâëÿþ â ñëîâàðü
            towers[currentTilePosition] = tower;
            PlayRandomBuildSound();
        }
        else { Debug.Log("Íå óäàëîñü óñòàíîâèòü áàøíþ, òàê êàê prefab is null"); }

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



    // Ìåòîä äëÿ ïðîèãðûâàíèÿ êîíêðåòíîãî çâóêà ïî èíäåêñó â ìàññèâå
    void PlaySound(int soundIndex)
    {
        // Ïðîâåðÿåì, ÷òîáû èíäåêñ íå âûõîäèë çà ïðåäåëû ìàññèâà
        if (soundIndex >= 0 && soundIndex < soundClips.Length)
        {
            // Óñòàíàâëèâàåì âûáðàííûé çâóê â Audio Source
            audioSource.clip = soundClips[soundIndex];

            // Ïðîèãðûâàåì çâóê
            audioSource.Play();
        }
        else
        {
            // Âûâîäèì ïðåäóïðåæäåíèå â êîíñîëü, åñëè èíäåêñ íåêîððåêòåí
            Debug.LogWarning("Invalid sound index.");
        }
    }

    void PlayRandomBuildSound()
    {
        // Ãåíåðèðóåì ñëó÷àéíûé èíäåêñ
        int randomIndex = Random.Range(1, soundClips.Length);

        // Óñòàíàâëèâàåì âûáðàííûé çâóê â Audio Source
        audioSource.clip = soundClips[randomIndex];

        // Ïðîèãðûâàåì çâóê
        audioSource.Play();
    }
}












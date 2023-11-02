using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;


    private Dictionary<Vector3Int, GameObject> towers = new Dictionary<Vector3Int, GameObject>();

    [SerializeField] private GameObject ballistaPrefab;

    private Tilemap grassTilemap; 




    void Start()
    {
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

        Debug.Log($"grassTilemap is null? = {grassTilemap == null}");
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
        if (towers.ContainsKey(tilePosition))
        {
            Debug.Log("Это место занято другой постройкой");
        }
        else 
        {
            SetTower(tilePosition);
        }
    }





    private void SetTower(Vector3Int tilePosition)
    {
        if (ballistaPrefab != null)
        {
            // Инстанцирую баллисту
            GameObject ballista = Instantiate(ballistaPrefab);

            // Устанавливаю позицию баллисте
            ballista.transform.position = GetCenterTilePositionInWorld(tilePosition);

            // Добавляю в словарь
            towers[tilePosition] = ballista;
        }
        else { Debug.Log("Не удалось установить баллисту, так как ballistaPrefab is null"); }
    }





    private Vector3 GetCenterTilePositionInWorld(Vector3Int tilePosition)
    {
        return grassTilemap.CellToWorld(tilePosition) + grassTilemap.cellSize * 0.5f;
    }
}

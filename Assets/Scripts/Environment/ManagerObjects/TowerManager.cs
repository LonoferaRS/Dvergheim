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
            Debug.Log("��� ����� ������ ������ ����������");
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
            // ����������� ��������
            GameObject ballista = Instantiate(ballistaPrefab);

            // ������������ ������� ��������
            ballista.transform.position = GetCenterTilePositionInWorld(tilePosition);

            // �������� � �������
            towers[tilePosition] = ballista;
        }
        else { Debug.Log("�� ������� ���������� ��������, ��� ��� ballistaPrefab is null"); }
    }





    private Vector3 GetCenterTilePositionInWorld(Vector3Int tilePosition)
    {
        return grassTilemap.CellToWorld(tilePosition) + grassTilemap.cellSize * 0.5f;
    }
}

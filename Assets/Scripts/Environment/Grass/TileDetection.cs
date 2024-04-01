using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDetection : MonoBehaviour
{

    [SerializeField] private GameObject DecorativeTilemapObject;
    [SerializeField] private TileBase grassTilePrefab;

    private Tilemap grassTilemap;
    private Tilemap decorativeTilemap;



    void Start()
    {
        grassTilemap = GetComponentInParent<Tilemap>();
        decorativeTilemap = DecorativeTilemapObject.GetComponent<Tilemap>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (grassTilemap != null)
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tilePosition = grassTilemap.WorldToCell(clickPosition);
                TileBase grassTile = grassTilemap.GetTile(tilePosition);
                TileBase decorativeTile = decorativeTilemap.GetTile(tilePosition);

                if (decorativeTile != null)
                {
                    // ������� ������������ ����
                    decorativeTilemap.SetTile(tilePosition, null);

                    // ��������� ���� ������
                    grassTilemap.SetTile(tilePosition, grassTilePrefab);
                }

                if (grassTile != null || decorativeTile != null)
                {
                    // TowerManager �������� ������� ���� �������
                    TowerManager.instance.ReciveTilePosition(tilePosition);
                }
            }
            else { Debug.Log("��������� ���������� ������� �����, ��� ��� Tilemap is null"); }
        }
    }
}

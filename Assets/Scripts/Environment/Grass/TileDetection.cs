using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDetection : MonoBehaviour
{

    private Tilemap tileMap;



    void Start()
    {
        tileMap = GetComponentInParent<Tilemap>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tileMap != null)
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int tilePosition = tileMap.WorldToCell(clickPosition);

                TileBase tile = tileMap.GetTile(tilePosition);

                if (tile != null) 
                {
                    TowerManager.instance.ReciveTilePosition(tilePosition);
                }
            }
            else { Debug.Log("Неудалось определить позицию тайла, так как Tilemap is null"); }
        }
    }
}

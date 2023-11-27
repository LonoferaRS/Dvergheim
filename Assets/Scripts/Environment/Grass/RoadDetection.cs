// Ваш скрипт с TilemapClickSound

using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClickSound : MonoBehaviour
{
    public Tilemap tilemap;
    public AudioClip clickSound; // Звук для воспроизведения

    void Update()
    {
        // Проверка нажатия левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Проверка
            if (!TowerManager.isAnyPanelIsActive)
            {
                // Получение позиции курсора в мировых координатах
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                // Получение тайла в указанной ячейке
                TileBase tile = tilemap.GetTile(cellPosition);

                // Если тайл существует, проигрываем звук
                if (tile != null)
                {
                    // Получаем компонент AudioSource с игровой камеры
                    AudioSource cameraAudioSource = Camera.main.GetComponent<AudioSource>();

                    // Проверяем, есть ли компонент AudioSource
                    if (cameraAudioSource != null)
                    {
                        // Проигрываем указанный звук
                        cameraAudioSource.PlayOneShot(clickSound);
                    }
                    else
                    {
                        Debug.LogError("AudioSource не найден на игровой камере!");
                    }
                }
            }
        }
    }
}

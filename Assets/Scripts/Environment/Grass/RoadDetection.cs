// ��� ������ � TilemapClickSound

using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClickSound : MonoBehaviour
{
    public Tilemap tilemap;
    public AudioClip clickSound; // ���� ��� ���������������

    void Update()
    {
        // �������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            // ��������
            if (!TowerManager.isAnyPanelIsActive)
            {
                // ��������� ������� ������� � ������� �����������
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                // ��������� ����� � ��������� ������
                TileBase tile = tilemap.GetTile(cellPosition);

                // ���� ���� ����������, ����������� ����
                if (tile != null)
                {
                    // �������� ��������� AudioSource � ������� ������
                    AudioSource cameraAudioSource = Camera.main.GetComponent<AudioSource>();

                    // ���������, ���� �� ��������� AudioSource
                    if (cameraAudioSource != null)
                    {
                        // ����������� ��������� ����
                        cameraAudioSource.PlayOneShot(clickSound);
                    }
                    else
                    {
                        Debug.LogError("AudioSource �� ������ �� ������� ������!");
                    }
                }
            }
        }
    }
}

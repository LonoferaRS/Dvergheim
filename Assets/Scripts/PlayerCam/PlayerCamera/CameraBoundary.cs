using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    private Collider2D tileCollider;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        tileCollider = GetComponent<Collider2D>();
        if (tileCollider == null)
        {
            Debug.LogError("Tile Collider not found!");
        }
    }

    void LateUpdate()
    {
        // �������� ������� ������� � ������� ������
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraSize = mainCamera.orthographicSize;

        // ����������� ���������� �������� ������������ � ���������� ���������� ������������ ����������
        Vector3 localCameraPosition = tileCollider.transform.InverseTransformPoint(cameraPosition);

        // ������������ ���������� � �������� ������ ����������
        float clampedX = Mathf.Clamp(localCameraPosition.x, tileCollider.bounds.min.x + cameraSize, tileCollider.bounds.max.x - cameraSize);
        float clampedY = Mathf.Clamp(localCameraPosition.y, tileCollider.bounds.min.y + cameraSize, tileCollider.bounds.max.y - cameraSize);

        // ����������� ������� � ������� ����������
        Vector3 clampedCameraPosition = tileCollider.transform.TransformPoint(new Vector3(clampedX, clampedY, localCameraPosition.z));

        // ������������� ����� ������� ������
        mainCamera.transform.position = clampedCameraPosition;
    }
}

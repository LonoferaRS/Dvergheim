using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCamera : MonoBehaviour
{
    public float baseMoveSpeed = 1f;
    public float maxMoveSpeed = 50f;
    public float accelerationMultiplier = 0.35f;
    public float zoomSpeed = 5f;

    private Camera mainCamera;
    private float currentMoveSpeed;
    
    [SerializeField] private Tilemap tilemap;
    private Collider2D tilemapCollider;

    private float minOrthographicSize = 1f;
    private float maxOrthographicSize = 10f;







    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        currentMoveSpeed = baseMoveSpeed;

        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
    }
    
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentMoveSpeed += accelerationMultiplier;
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
        }

        // ����������� ������������ �������� ��������
        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, baseMoveSpeed, maxMoveSpeed);

        // ����������� ������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * currentMoveSpeed * Time.deltaTime;

        // ���������� ����� ������� ������ � ������ �������
        Vector3 newPosition = transform.position + moveDirection;

        if (tilemapCollider != null)
        {
            // ���������� �������������� ������������� ����������
            Bounds tilemapBounds = tilemapCollider.bounds;

            // ������������ ����� ������� �� ������� ����������
            newPosition.x = Mathf.Clamp(newPosition.x, tilemapBounds.min.x + mainCamera.orthographicSize * mainCamera.aspect, tilemapBounds.max.x - mainCamera.orthographicSize * mainCamera.aspect);
            newPosition.y = Mathf.Clamp(newPosition.y, tilemapBounds.min.y + mainCamera.orthographicSize, tilemapBounds.max.y - mainCamera.orthographicSize);
        }

        transform.position = newPosition;



        // ����������� ��������� ������� ������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newOrthographicSize = mainCamera.orthographicSize - scrollInput * zoomSpeed;

        // ������������ ��������� ������� ������ �� �������� �����
        newOrthographicSize = Mathf.Clamp(newOrthographicSize, minOrthographicSize, maxOrthographicSize);

        mainCamera.orthographicSize = newOrthographicSize;

        // ������ ��������� ������� � ������ ������ ������ ������ ���� �� ������� �� �� �������
        if (mainCamera.orthographicSize <= maxOrthographicSize && mainCamera.orthographicSize >= minOrthographicSize)
        {
            // ���������� �������������� ������������� ����������
            Bounds tilemapBounds = tilemapCollider.bounds;

            // ���������, ����� ����� ������ �� �������� �� ������� �����
            float newWidth = mainCamera.aspect * mainCamera.orthographicSize * 2;
            newWidth = Mathf.Clamp(newWidth, tilemapBounds.min.x * 2, tilemapBounds.max.x * 2);

            // ������������� ����� ������ ������
            mainCamera.orthographicSize = newWidth / (2 * mainCamera.aspect);
        }
    }
}

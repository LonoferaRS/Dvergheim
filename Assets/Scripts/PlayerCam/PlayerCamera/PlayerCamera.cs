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

        // ќграничение максимальной скорости движени€
        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, baseMoveSpeed, maxMoveSpeed);

        // ѕеремещение камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * currentMoveSpeed * Time.deltaTime;

        // ќпредел€ем новую позицию камеры с учетом границы
        Vector3 newPosition = transform.position + moveDirection;

        if (tilemapCollider != null)
        {
            // ќпредел€ем ограничивающий пр€моугольник коллайдера
            Bounds tilemapBounds = tilemapCollider.bounds;

            // ќграничиваем новую позицию по области коллайдера
            newPosition.x = Mathf.Clamp(newPosition.x, tilemapBounds.min.x + mainCamera.orthographicSize * mainCamera.aspect, tilemapBounds.max.x - mainCamera.orthographicSize * mainCamera.aspect);
            newPosition.y = Mathf.Clamp(newPosition.y, tilemapBounds.min.y + mainCamera.orthographicSize, tilemapBounds.max.y - mainCamera.orthographicSize);
        }

        transform.position = newPosition;



        // ќграничение изменени€ размера камеры
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newOrthographicSize = mainCamera.orthographicSize - scrollInput * zoomSpeed;

        // ќграничиваем изменение размера камеры по границам карты
        newOrthographicSize = Mathf.Clamp(newOrthographicSize, minOrthographicSize, maxOrthographicSize);

        mainCamera.orthographicSize = newOrthographicSize;

        // “еперь учитываем границы и мен€ем размер камеры только если не выходим за их пределы
        if (mainCamera.orthographicSize <= maxOrthographicSize && mainCamera.orthographicSize >= minOrthographicSize)
        {
            // ќпредел€ем ограничивающий пр€моугольник коллайдера
            Bounds tilemapBounds = tilemapCollider.bounds;

            // ѕровер€ем, чтобы нова€ ширина не выходила за границы карты
            float newWidth = mainCamera.aspect * mainCamera.orthographicSize * 2;
            newWidth = Mathf.Clamp(newWidth, tilemapBounds.min.x * 2, tilemapBounds.max.x * 2);

            // ”станавливаем новый размер камеры
            mainCamera.orthographicSize = newWidth / (2 * mainCamera.aspect);
        }
    }
}

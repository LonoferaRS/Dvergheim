using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float baseMoveSpeed = 1f;
    public float maxMoveSpeed = 50f;
    public float accelerationMultiplier = 0.35f;
    public float zoomSpeed = 5f;

    private Camera mainCamera;
    private float currentMoveSpeed;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        currentMoveSpeed = baseMoveSpeed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentMoveSpeed += accelerationMultiplier;
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
        }

        // Ограничение максимальной скорости движения
        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, baseMoveSpeed, maxMoveSpeed);

        // Перемещение камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * currentMoveSpeed * Time.deltaTime;
        transform.position += moveDirection;

        // Изменение масштаба камеры
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        mainCamera.orthographicSize -= scrollInput * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 1f, Mathf.Infinity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float zoomSpeed = 5f;
    public float multiplier = 0.0005f;
    public float[] borders;
    float coefx = 1.776f;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moveSpeed = moveSpeed + multiplier;
        }
        else
        {
            moveSpeed = 3f;
        }


        // Перемещение камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + moveDirection;
        if (newPosition.x-mainCamera.orthographicSize*coefx < borders[0])
        {
            newPosition = new Vector3(borders[0]+ mainCamera.orthographicSize * coefx, newPosition.y, newPosition.z);
        }
        if (newPosition.x+ mainCamera.orthographicSize * coefx > borders[2])
        {
            newPosition = new Vector3(borders[2]- mainCamera.orthographicSize * coefx, newPosition.y, newPosition.z);
        }
        if (newPosition.y + mainCamera.orthographicSize > borders[1])
        {
            newPosition = new Vector3(newPosition.x, borders[1]- mainCamera.orthographicSize, newPosition.z);
        }
        if (newPosition.y - mainCamera.orthographicSize < borders[3])
        {
            newPosition = new Vector3(newPosition.x, borders[3] + mainCamera.orthographicSize, newPosition.z);
        }
        transform.position = newPosition;
        // Изменение масштаба камеры
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        mainCamera.orthographicSize -= scrollInput * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 1f, Mathf.Infinity);
    }
}

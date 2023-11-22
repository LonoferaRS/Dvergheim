using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Camera cam;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        canvasRectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ��������� ������� � �������� Canvas, ����� ��� ��������������� ������
        canvasRectTransform.sizeDelta = new Vector2(cam.pixelWidth, cam.pixelHeight);
        Vector3 canvasPos = cam.WorldToScreenPoint(transform.position);
        canvasRectTransform.position = canvasPos;
    }
}
using UnityEngine;

public class MainTower : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // ��������, ��� � ������� ����� ������ � ����� "Enemy"
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enter");
            // ���������� ������ �����
            Destroy(other.gameObject);
        }
    }
}

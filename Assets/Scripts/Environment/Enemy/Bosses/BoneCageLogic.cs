using UnityEngine;

public class BoneShield : MonoBehaviour
{
    public Transform necromancer; // ������ �� ����������, �� ������� ����� ��������� ���
    public float followSpeed = 5f; // �������� ����������

    private void Update()
    {
        if (necromancer != null)
        {
            // ��������� ����������� � ����������
            Vector3 direction = necromancer.position - transform.position;
            direction.y = 0f; // ���������� ������, ����� ��� �� �������� ����� � ����

            // ��������� ����� ������� ��� ����
            Vector3 newPosition = transform.position + direction.normalized * followSpeed * Time.deltaTime;

            // ���������� ��� � ����� �������
            transform.position = newPosition;
        }
    }
}


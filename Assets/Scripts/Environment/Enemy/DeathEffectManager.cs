using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    [SerializeField] private AudioClip[] deathSound; // ������ ������������ ������

    private AudioSource audioSource;
    private float lifeTime = 2f; // ����� ����� ������� ����� ������ �������

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ����������� ���� ������

        if (deathSound.Length > 0)
        {
            PlayRandomSound();
        }
        else
        {
            Debug.LogError("No death sounds assigned!");
        }

        // ��������� ������ ��� ������������ �������
        Invoke("DestroyEffect", lifeTime);
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, deathSound.Length);
        audioSource.clip = deathSound[randomIndex];
        audioSource.volume = PlayerPrefs.GetInt("SfxDeath") * PlayerPrefs.GetFloat("SfxVolume");
        audioSource.Play();
    }

    void DestroyEffect()
    {
        Destroy(gameObject);
    }
}

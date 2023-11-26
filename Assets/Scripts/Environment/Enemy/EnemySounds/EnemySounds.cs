using UnityEngine;

public class GoblinSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] goblinSounds; // ������ ������������ �������

    public float minTimeBetweenSounds = 5f; // ����������� ����� ����� �������
    public float maxTimeBetweenSounds = 10f; // ������������ ����� ����� �������

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (goblinSounds.Length > 0)
        {
            PlayRandomSound();
        }
        else
        {
            Debug.LogError("No goblin sounds assigned!");
        }
    }

    void Update()
    {
        // ���������, ������ �� ����������� ����� ��� ������������ ���������� �����
        if (!audioSource.isPlaying)
        {
            float randomTime = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
            Invoke("PlayRandomSound", randomTime);
        }
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, goblinSounds.Length);
        audioSource.clip = goblinSounds[randomIndex];
        audioSource.Play();
    }
}

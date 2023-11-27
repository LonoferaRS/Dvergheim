using UnityEngine;

public class GoblinSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] goblinSounds; // ћассив аудиодорожек гоблина

    public float minTimeBetweenSounds = 5f; // ћинимальное врем€ между звуками
    public float maxTimeBetweenSounds = 10f; // ћаксимальное врем€ между звуками

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
        // ѕровер€ем, прошло ли достаточное врем€ дл€ проигрывани€ следующего звука
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

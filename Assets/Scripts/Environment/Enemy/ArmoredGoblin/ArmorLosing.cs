using UnityEngine;

public class ArmorLosing : MonoBehaviour
{
    [SerializeField] private AudioClip[] deathSound; // Массив аудиодорожек

    private AudioSource audioSource;
    private float lifeTime = 2f; // Время жизни эффекта

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Проигрываем звук 

        if (deathSound.Length > 0)
        {
            PlayRandomSound();
        }
        else
        {
            Debug.LogError("No sounds assigned!");
        }

        // Запускаем таймер для исчезновения эффекта
        Invoke("DestroyEffect", lifeTime);
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, deathSound.Length);
        audioSource.clip = deathSound[randomIndex];
        audioSource.Play();
    }

    void DestroyEffect()
    {
        Destroy(gameObject);
    }
}

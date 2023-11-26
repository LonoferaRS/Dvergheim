using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicTracks; // Массив аудиодорожек
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicTracks.Length > 0)
        {
            PlayRandomMusic();
        }
        else
        {
            Debug.LogError("No music tracks assigned!");
        }
    }

    void PlayRandomMusic()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();
    }
}
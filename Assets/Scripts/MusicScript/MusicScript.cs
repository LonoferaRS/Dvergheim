using UnityEngine;
using UnityEngine.Audio;

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
    private void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
    void PlayRandomMusic()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();
    }
}
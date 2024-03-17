using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] normalMusicTracks; // ћассив обычной музыки
    public AudioClip[] bossMusicTracks;   // ћассив музыки дл€ босс-битвы
    private AudioSource audioSource;

    private void Start()
    {
        // ѕолучаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

        // Ќачинаем воспроизведение случайной трека обычной музыки
        PlayRandomNormalTrack();
    }
    public void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume") * PlayerPrefs.GetInt("MusicOff");
    }
    // ћетод дл€ проигрывани€ случайного трека обычной музыки
    void PlayRandomNormalTrack()
    {
        int randomIndex = Random.Range(0, normalMusicTracks.Length);
        audioSource.clip = normalMusicTracks[randomIndex];
        audioSource.Play();
    }

    // ћетод дл€ проигрывани€ конкретного трека босс-музыки
    void PlayBossTrack(int bossTrackIndex)
    {
        // ѕровер€ем, чтобы индекс не выходил за пределы массива
        if (bossTrackIndex >= 0 && bossTrackIndex < bossMusicTracks.Length)
        {
            audioSource.clip = bossMusicTracks[bossTrackIndex];
            audioSource.Play();
        }
        else
        {
            // ¬ыводим предупреждение в консоль, если индекс некорректен
            Debug.LogWarning("Invalid boss track index.");
        }
    }

    // ѕример использовани€ метода дл€ смены трека, например, при по€влении босса
    void BossAppeared(int bossTrackIndex)
    {
        // ќстанавливаем текущую музыку и проигрываем трек босс-битвы
        audioSource.Stop();
        PlayBossTrack(bossTrackIndex);
    }

    // ѕример использовани€ метода дл€ смены трека, например, при смерти босса
    void BossDefeated()
    {
        // ¬озвращаемс€ к случайной нормальной музыке
        PlayRandomNormalTrack();
    }
}

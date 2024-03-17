using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] normalMusicTracks; // ������ ������� ������
    public AudioClip[] bossMusicTracks;   // ������ ������ ��� ����-�����
    private AudioSource audioSource;

    private void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();

        // �������� ��������������� ��������� ����� ������� ������
        PlayRandomNormalTrack();
    }
    public void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume") * PlayerPrefs.GetInt("MusicOff");
    }
    // ����� ��� ������������ ���������� ����� ������� ������
    void PlayRandomNormalTrack()
    {
        int randomIndex = Random.Range(0, normalMusicTracks.Length);
        audioSource.clip = normalMusicTracks[randomIndex];
        audioSource.Play();
    }

    // ����� ��� ������������ ����������� ����� ����-������
    void PlayBossTrack(int bossTrackIndex)
    {
        // ���������, ����� ������ �� ������� �� ������� �������
        if (bossTrackIndex >= 0 && bossTrackIndex < bossMusicTracks.Length)
        {
            audioSource.clip = bossMusicTracks[bossTrackIndex];
            audioSource.Play();
        }
        else
        {
            // ������� �������������� � �������, ���� ������ �����������
            Debug.LogWarning("Invalid boss track index.");
        }
    }

    // ������ ������������� ������ ��� ����� �����, ��������, ��� ��������� �����
    void BossAppeared(int bossTrackIndex)
    {
        // ������������� ������� ������ � ����������� ���� ����-�����
        audioSource.Stop();
        PlayBossTrack(bossTrackIndex);
    }

    // ������ ������������� ������ ��� ����� �����, ��������, ��� ������ �����
    void BossDefeated()
    {
        // ������������ � ��������� ���������� ������
        PlayRandomNormalTrack();
    }
}

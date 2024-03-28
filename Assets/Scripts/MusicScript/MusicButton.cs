using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Toggle music_toggle;
    public AudioSource musicSource;

    public Sprite playSprite; // ���� ��� ������� ��������������� ������
    public Sprite pauseSprite; // ���� ��� ������� ������������ ������

    private Image buttonImage; // ����������� ����

    private void Start()
    {
        buttonImage = GetComponent<Image>(); // �������� ��������� Image ��� ������
        musicSource.Play();
        UpdateButtonImage();

        // ������������� ����������� �����������
        if (buttonImage != null && playSprite != null)
        {
            buttonImage.sprite = playSprite;
        }
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt("MusicOff", PlayerPrefs.GetInt("MusicOff") == 1 ? 0:1);
        music_toggle.isOn = PlayerPrefs.GetInt("MusicOff") == 1;
        UpdateButtonImage();
    }

    public void UpdateButtonImage()
    {
        // �������� ����������� ������ � ����������� �� ��������� ������
        if (buttonImage != null) // ��������, ��� ��������� Image ��� �������
        {
            if (PlayerPrefs.GetInt("MusicOff")== 1)
            {
                buttonImage.sprite = playSprite;
            }
            else
            {
                buttonImage.sprite = pauseSprite;
            }
        }
    }
}

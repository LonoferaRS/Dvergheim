using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
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
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }

        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        // �������� ����������� ������ � ����������� �� ��������� ������
        if (buttonImage != null) // ��������, ��� ��������� Image ��� �������
        {
            if (musicSource.isPlaying)
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

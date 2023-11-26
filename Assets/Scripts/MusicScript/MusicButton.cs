using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;

    public Sprite playSprite; // Поле для спрайта воспроизведения музыки
    public Sprite pauseSprite; // Поле для спрайта приостановки музыки

    private Image buttonImage; // Добавленное поле

    private void Start()
    {
        buttonImage = GetComponent<Image>(); // Получаем компонент Image при старте
        musicSource.Play();
        UpdateButtonImage();

        // Устанавливаем изначальное изображение
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
        // Изменяем изображение кнопки в зависимости от состояния музыки
        if (buttonImage != null) // Убедимся, что компонент Image был получен
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

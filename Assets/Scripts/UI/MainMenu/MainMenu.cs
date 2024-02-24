using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float ButtonDelay = 0.2f;
   public void PlayGame()
    {
        StartCoroutine(Load(1));
    }
    public void ExitGame()
    {
        StartCoroutine(Quit());
    }
    public void Options()
    {
        StartCoroutine(Load(2));
    }
    void PlayButtonSound()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SfxVolume");
        GetComponent<AudioSource>().Play();
    }
    IEnumerator Load(int x) 
    {
        PlayButtonSound();
        yield return new WaitForSeconds(ButtonDelay);
        SceneManager.LoadScene(x);
    }
    IEnumerator Quit()
    {
        PlayButtonSound();
        yield return new WaitForSeconds(ButtonDelay);
        Application.Quit();
    }
}


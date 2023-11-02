using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
   public void Playgame()
    {
         SceneManager.LoadScene(1);
    }
    public void Quit()
    {
            Application.Quit();
    }
    public void Options()
    {
        SceneManager.LoadScene(2);
    }
}

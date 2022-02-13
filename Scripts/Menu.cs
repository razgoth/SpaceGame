using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameMode");
    }

    public void Control()
    {
        SceneManager.LoadScene("Controls");
    }

        public void Score()
    {
        SceneManager.LoadScene("Score");
    }

  

    public void QuitGame()
    {
        Application.Quit();
    }

}

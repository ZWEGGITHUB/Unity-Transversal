using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


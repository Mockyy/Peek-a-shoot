using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Charge une scène
    public void LoadScene(string newScene)
    {
        //Si c'est une autre scène que le jeu principal, on débloque la souris
        if (newScene != "FPS")
        {
            Cursor.lockState = CursorLockMode.None;
        }

        SceneManager.LoadScene(newScene);
    }

    //Fermeture de l'application
    public void Quit()
    {
        Application.Quit();
    }
}

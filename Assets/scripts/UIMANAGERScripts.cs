using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMANAGERScripts : MonoBehaviour
{
    public GameObject MenuOptions;

    public GameObject Pause;
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OptionsMenu()
    {
        MenuOptions.SetActive(true);
    }

    public void MainMenu()
    {
        MenuOptions.SetActive(false);
        Debug.Log("mainmenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void PauseGame()
    {
        Pause.SetActive(true);
    }

    public void ReplayGame()
    {
        Pause.SetActive(false);
    }

    public void BackToPause()
    {
        MenuOptions.SetActive(false);
    }
}

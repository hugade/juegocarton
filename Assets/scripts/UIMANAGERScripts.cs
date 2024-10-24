using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIMANAGERScripts : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    void Start()
    {
        Application.targetFrameRate = 60;

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }

    public GameObject MenuOptions;

    public GameObject Pause;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsMenu()
    {
        MenuOptions.SetActive(true);
    }

    public void MainMenu()
    {
        MenuOptions.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void PauseGame()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BackToGame()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToPause()
    {
        MenuOptions.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void SetVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetVolume();
    }
}

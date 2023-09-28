using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider slider;


    private void Start()
    {
        // ??c m?c âm l??ng t? PlayerPrefs khi kh?i ??ng
        float savedVolume = PlayerPrefs.GetFloat("Volume",1f);
        slider.value = savedVolume;
        SetVolume(savedVolume);
    }
    private void Update()
    {
        AudioListener.volume = slider.value;
      
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Debug.Log("aquiafdoi");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        // C?p nh?t âm l??ng và l?u vào PlayerPrefs
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

}

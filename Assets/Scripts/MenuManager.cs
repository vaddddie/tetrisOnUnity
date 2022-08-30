using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject records;
    [SerializeField] private GameObject settings;

    [SerializeField] private new AudioClip[] audio;
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle fallToggle;

    private void Awake()
    {
        PlayerPrefs.SetInt("Skin1", 1);
        PlayerPrefs.SetInt("Music1", 1);
        PlayerPrefs.SetInt("Deference1", 1);

        if (DataHolder.Get())
        {
            mainMenu.SetActive(false);
            shop.SetActive(true);
            records.SetActive(false);
            settings.SetActive(false);
            DataHolder.Set(false);
        } else
        {
            mainMenu.SetActive(true);
            shop.SetActive(false);
            records.SetActive(false);
            settings.SetActive(false);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicVolume", 1) == 1)
        {
            musicToggle.isOn = true;
            
            ToggleMusic(true);
        } else
        {
            musicToggle.isOn = false;
            
            ToggleMusic(false);
        }

        if (PlayerPrefs.GetInt("SoundVolume", 1) == 1)
        {
            soundToggle.isOn = true;
            
            ToggleSound(true);
        } else
        {
            soundToggle.isOn = false;
            
            ToggleSound(false);
        }

        if (PlayerPrefs.GetInt("DoubleTap", 1) == 1)
        {
            fallToggle.isOn = true;
        } else
        {
            fallToggle.isOn = false;
        }

        backgroundMusic.clip = audio[Random.Range(0, audio.Length)];
        backgroundMusic.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("MusicVolume", 0);
            PlayerPrefs.SetInt("MusicVolume", 1);
        } else
        {
            mixer.audioMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetInt("MusicVolume", 0);
        }

        PlayerPrefs.Save();
    }

    public void ToggleSound(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("SoundVolume", 0);
            PlayerPrefs.SetInt("SoundVolume", 1);
        } else
        {
            mixer.audioMixer.SetFloat("SoundVolume", -80);
            PlayerPrefs.SetInt("SoundVolume", 0);
        }

        PlayerPrefs.Save();
    }

    public void ToggleFall(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetInt("DoubleTap", 1);
        } else
        {
            PlayerPrefs.SetInt("DoubleTap", 0);
        }

        PlayerPrefs.Save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

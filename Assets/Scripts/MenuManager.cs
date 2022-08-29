using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject records;

    private void Awake()
    {
        PlayerPrefs.SetInt("Skin1", 1);

        if (DataHolder.Get())
        {
            mainMenu.SetActive(false);
            shop.SetActive(true);
            records.SetActive(false);
            DataHolder.Set(false);
        } else
        {
            mainMenu.SetActive(true);
            shop.SetActive(false);
            records.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

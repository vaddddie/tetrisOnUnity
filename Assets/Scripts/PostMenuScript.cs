using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DataHolder;

public class PostMenuScript : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ShopLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        DataHolder.Set(true);
    }
}

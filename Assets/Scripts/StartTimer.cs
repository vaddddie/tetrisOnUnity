using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private TimerScript timerScript;
    [SerializeField] private MusicScript musicScript;

    [SerializeField] private Text timeText;

    [SerializeField] private GameObject startTimer;
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private GameObject interface_;
    [SerializeField] private GameObject pauseButton;

    private float timerDelay = 0.7f;

    public void Start()
    {
        Time.timeScale = 1;
        timeText.text = "3";
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        for (int i = 3; i > 0; i--)
        {
            timeText.text = i.ToString();

            yield return new WaitForSeconds(timerDelay);
        }

        timeText.text = "GO!";

        yield return new WaitForSeconds(timerDelay);

        startTimer.SetActive(false);
        timerScript.StartTimer();
        darkPanel.SetActive(false);
        interface_.SetActive(true);
        musicScript.MusicStart();
        pauseButton.SetActive(true);
        gameManager.InitState();
    }

}

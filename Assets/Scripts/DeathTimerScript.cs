using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTimerScript : MonoBehaviour
{
    public Image timer;

    public GameObject adsMenu;
    public GameObject restartMenu;

    public WritingScript writingScript;

    private Coroutine timerCoroutine;

    void Start()
    {
        timer.fillAmount = 1f;
    }

    public void StartTimer()
    {
        timerCoroutine = StartCoroutine(DeathTimer());
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.03f);

        while(timer.fillAmount > 0)
        {
            timer.fillAmount -= 0.01f;

            yield return new WaitForSeconds(0.03f);

            if (Input.GetKey("q"))
            {
                if (timer.fillAmount >= 0.1f)
                {
                    timer.fillAmount -= 0.1f;
                } else
                {
                    timer.fillAmount = 0;
                }
            }
        }

        adsMenu.SetActive(false);
        restartMenu.SetActive(true);

        writingScript.WrittingInData();
    }   
}

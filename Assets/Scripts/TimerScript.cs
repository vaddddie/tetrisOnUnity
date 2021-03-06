using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;

    private int Sec = 0;
    private int Min = 0;

    void Start()
    {
        timerText.text = Min.ToString("D2") + ":" + Sec.ToString("D2");
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        
            if (Sec < 60)
            {
                Sec += 1;
            } else
            {
                Sec = 0;
                Min += 1;
            }

            if (Min != 60)
            {
                timerText.text = Min.ToString("D2") + ":" + Sec.ToString("D2");
            } else
            {
                timerText.text = "WTF MAN>?";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WritingScript : MonoBehaviour
{
    public Text score;
    public Text lines;
    public Text time;

    public ScoreScript scoreScript;
    public LineCounterScript lineCounterScript;
    public TimerScript timerScript;

    private int newScore;

    public void WrittingInData()
    {
        newScore = scoreScript.Score;

        score.text = newScore.ToString("D7");
        lines.text = lineCounterScript.lineCounter.ToString();
        time.text = timerScript.Min.ToString("D2") + ":" + timerScript.Sec.ToString("D2");

        if (PlayerPrefs.GetInt("R5", 0) < newScore)
        {
            NewRecord();
        }
    }

    private void NewRecord()
    {
        for (int i = 4; i > -1; i--)
        {
            if (PlayerPrefs.GetInt($"R{i}", 0) >= newScore | i == 0)
            {
                for (int j = i + 2; j < 6; j++)
                {
                    PlayerPrefs.SetInt($"R{j}", PlayerPrefs.GetInt($"R{j - 1}", 0));
                }
                PlayerPrefs.SetInt($"R{i + 1}", newScore);

                break;
            }
        }
    }
}

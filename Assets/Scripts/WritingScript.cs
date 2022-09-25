using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WritingScript : MonoBehaviour
{
    public Text score;
    public Text lines;
    public Text time;
    public Text gold;

    public ScoreScript scoreScript;
    public LineCounterScript lineCounterScript;
    public TimerScript timerScript;

    private int newScore;
    private int newLines;
    private int newGold;

    public void WrittingInData()
    {
        newScore = scoreScript.Score;
        newLines = lineCounterScript.lineCounter;

        score.text = newScore.ToString("D7");
        lines.text = lineCounterScript.lineCounter.ToString();
        time.text = timerScript.Min.ToString("D2") + ":" + timerScript.Sec.ToString("D2");

        GoldAdding();

        if (PlayerPrefs.GetInt("R5", 0) < newScore)
        {
            NewRecord();
        }
    }

    private void GoldAdding()
    {
        if (newLines != 0)
        {
            newGold = (int)Mathf.Floor(newScore / 100);
        } else
        {
            newGold = (int)Mathf.Floor(newScore / 500);
        }

        gold.text = newGold.ToString();
        PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold", 0) + newGold);
    }

    private void NewRecord()
    {
        for (int i = 4; i > -1; i--)
        {
            if (PlayerPrefs.GetInt($"R{i}", 0) >= newScore | i == 0)
            {
                for (int j = 5; j > 0; j--)
                {
                    if ((j - 1) == i)
                    {
                        PlayerPrefs.SetInt($"R{j}", newScore);
                        break;
                    }

                    PlayerPrefs.SetInt($"R{j}", PlayerPrefs.GetInt($"R{j - 1}", 0));
                }

                break;
            }
        }

        PlayerPrefs.Save();
    }
}

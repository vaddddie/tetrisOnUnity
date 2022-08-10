using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int Score = 0;
    public Text textScore;

    void Start()
    {
        textScore.text = Score.ToString("D7");     
    }

    public void AddingPoints(int Number)
    {
        Score += Number;
        textScore.text = Score.ToString("D7");
    }

}

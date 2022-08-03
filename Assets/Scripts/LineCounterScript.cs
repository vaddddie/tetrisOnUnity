using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCounterScript : MonoBehaviour
{
    private int lineCounter = 0;
    public Text textLC;

    void Start()
    {
        textLC.text = lineCounter.ToString();
    }

    public void AddingCounter(int count)
    {
        lineCounter += count;
        textLC.text = lineCounter.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsScript : MonoBehaviour
{
    private Text record;

    private string value;

    private void Awake()
    {
        record = this.GetComponent<Text>();
        value = this.name;
    }

    public void Refresh()
    {
        if (PlayerPrefs.HasKey(value))
        {
            record.text = PlayerPrefs.GetInt(value, 0).ToString("D7"); 
        }
    }
}

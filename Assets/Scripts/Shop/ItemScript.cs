using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private string type;
    [SerializeField] private int id;
    private string name_;

    [SerializeField] private Text mainText;
    
    [SerializeField] private MoneyControllerScript moneyScript;

    private void Start()
    {
        name_ = this.name;
    }
    
    public void Tap()
    {
        if (PlayerPrefs.GetInt(name_, 0) == 0 & MoneyControllerScript.money >= price)
        {
            PlayerPrefs.SetInt(name_, 1);
            PlayerPrefs.Save();

            MoneyControllerScript.money -= price;
            moneyScript.ShopUpdate();
        } else if (PlayerPrefs.GetInt(name_, 0) == 1 & PlayerPrefs.GetInt(type + "Owned", 0) != id)
        {
            PlayerPrefs.SetInt(type + "Owned", id);
            PlayerPrefs.Save();
        }

    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(type + "Owned", 0) == id)
        {
            mainText.text = "Eqiped";
        } else if (PlayerPrefs.GetInt(name_, 0) == 1)
        {
            mainText.text = "Boughted";
        } else
        {
            mainText.text = price.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyControllerScript : MonoBehaviour
{
    public static int money;

    [SerializeField] private Text moneyText;
    
    void Start()
    {
        money = PlayerPrefs.GetInt("Gold", 0);

        ShopUpdate();
    }

    public void ShopUpdate()
    {
        PlayerPrefs.SetInt("Gold", money);
        moneyText.text = money.ToString();
    }
}

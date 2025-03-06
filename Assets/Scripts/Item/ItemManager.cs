using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{

    //public int coins;
    //public SOInt soCoins;
    public TextMeshProUGUI uiTextCoin;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void Reset()
    {
        //soCoins.value = 0;
        UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        //soCoins.value += amount;
    }

    private void UpdateUI()
    {
        //uiTextCoin.text = coins.ToString();
        //UiInGameManager.Instance.UpDateTextCoins(soCoins.value.ToString());
    }

}

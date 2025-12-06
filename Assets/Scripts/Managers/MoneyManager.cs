using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int _currentPlayerMoney;
    private string _moneyVariableString = "Player$$$";
    public static MoneyManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        if (PlayerPrefs.HasKey(_moneyVariableString))
            _currentPlayerMoney = PlayerPrefs.GetInt(_moneyVariableString);
        else
            PlayerPrefs.SetInt(_moneyVariableString, 0);
    }

    public void UpdatePlayerMoney(int amount)
    {
        _currentPlayerMoney += amount;
        PlayerPrefs.SetInt(_moneyVariableString, _currentPlayerMoney);
        GameEventsManager.instance.moneyEvents.MoneyAmountChanged(amount);
    }

    public int GetCurrentPlayerMoney()
    {
        return _currentPlayerMoney;
    }
}

using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int _currentPlayerMoney;
    private int _previousPlayerMoney;
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

        Lua.RegisterFunction("UpdateMoneyFromDialogue", this, SymbolExtensions.GetMethodInfo(() => UpdateMoneyFromDialogue(0)));
    }

    public void UpdatePlayerMoney(int amount)
    {
        _previousPlayerMoney = _currentPlayerMoney;
        _currentPlayerMoney += amount;

        Debug.Log("Had: " + _previousPlayerMoney);
        Debug.Log("Have: " + _currentPlayerMoney);
        PlayerPrefs.SetInt(_moneyVariableString, _currentPlayerMoney);
        GameEventsManager.instance.moneyEvents.MoneyAmountChanged(_previousPlayerMoney, _currentPlayerMoney, amount);
    }

    public void UpdateMoneyFromDialogue(double amount)
    {
        UpdatePlayerMoney((int)amount);
    }

    public int GetCurrentPlayerMoney()
    {
        return _currentPlayerMoney;
    }
}

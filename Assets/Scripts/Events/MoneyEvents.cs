using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEvents
{
    public event Action<int,int, int> onMoneyAmountChanged;

    public void MoneyAmountChanged(int previousBalance, int newBalance, int dollarAmount)
    {
        if (onMoneyAmountChanged != null)
            onMoneyAmountChanged(previousBalance, newBalance, dollarAmount);
    }
}

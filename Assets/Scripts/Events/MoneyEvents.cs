using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEvents
{
    public event Action<int,int> onMoneyAmountChanged;

    public void MoneyAmountChanged(int previousAmount, int newAmount)
    {
        if (onMoneyAmountChanged != null)
            onMoneyAmountChanged(previousAmount, newAmount);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEvents
{
    public event Action<int> onMoneyAmountChanged;

    public void MoneyAmountChanged(int amount)
    {
        if (onMoneyAmountChanged != null)
            onMoneyAmountChanged(amount);
    }
}

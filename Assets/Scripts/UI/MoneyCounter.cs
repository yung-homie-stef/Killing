using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerFundsNumber;
    [SerializeField] private int _FPS = 30;
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private string _numberFormat = "N0";
    private Coroutine _moneyCountCoroutine;


    public void UpdateBeforeCounting(int previousAmount, int newValue)
    {
        if (_moneyCountCoroutine != null) 
            StopCoroutine(_moneyCountCoroutine);
            
         _moneyCountCoroutine = StartCoroutine(CountMoney(previousAmount, newValue));
    }

    private IEnumerator CountMoney(int previousAmount, int newValue)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / _FPS);
        int previousValue = previousAmount;
        int stepAmount;

        if (newValue - previousValue < 0)
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (_FPS * _duration));
        else
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (_FPS * _duration));

        if (previousValue < newValue)
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                    previousValue = newValue;

                _playerFundsNumber.SetText(previousValue.ToString(_numberFormat));

                yield return wait;
            }
        else
            while (previousValue > newValue)
            {
                previousValue += stepAmount;
                if (previousValue < newValue)
                    previousValue = newValue;

                _playerFundsNumber.SetText(previousValue.ToString(_numberFormat));

                yield return wait;
            }
    }
        

}

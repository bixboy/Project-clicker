using System;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UsefulScript;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeStat> _upgradeList;
    [SerializeField] private int _coinAmount;

    public int GetCoinAmount() => _coinAmount;
    public event Action<int> OnCoinValueChanged;
    /*private void OnValidate()
    {
        _textGold.text = Scripts.NumberToString(_coinAmount, 6, 2);
    }*/

    public void CollectCoin(int amount)
    {
        _coinAmount += amount;
        OnCoinValueChanged?.Invoke(_coinAmount);
    }

    public void RemoveCoin(int amount)
    {
        _coinAmount = Mathf.Max(0, _coinAmount - amount);
    }

    public UpgradeStat GetUpgradeStatByName(StatName statName) => _upgradeList.Find(x => x.Name == statName);
    public Upgrade GetUpgradeByName(StatName statName) => GetUpgradeStatByName(statName).GetUpgrade();
    
    public bool Buy(StatName statName, int multi = 1)
    {
        int cost = GetUpgradeStatByName(statName).GetCostFromMultiplier(multi);
        if (_coinAmount < cost || GetUpgradeStatByName(statName).MaxAmountReached) { return false; }
        _coinAmount -= cost;
        OnCoinValueChanged?.Invoke(_coinAmount);
        GetUpgradeStatByName(statName).Buy(multi);
        return true;
    }
}

[Serializable]
public class UpgradeStat
{
    [SerializeField, Expandable] private Upgrade _upgrade;
    [SerializeField] private int _level;

    public Upgrade GetUpgrade() => _upgrade;
    public int GetLevel() => _level;

    public StatName Name => _upgrade.Name;
    
    public float Amount => Mathf.Min(_upgrade.StartAmount + _level * _upgrade.Amount, _upgrade.MaxAmount);

    public float NextAmount => Mathf.Min(_upgrade.StartAmount + (_level + 1) * _upgrade.Amount, _upgrade.MaxAmount);
    public float GetNextAmountFromMultiplier(int multi) => _upgrade.StartAmount + (_level + multi) * _upgrade.Amount;
    public int Cost => (int)(_upgrade.BaseCost * (_upgrade.CostMultiplier*_level + 1));

    public bool MaxAmountReached => Amount == NextAmount;

    public int GetCostFromMultiplier(int multi)
    {
        //Can be improved with maths
        int value = 0;
        for (int i = 0; i < multi; i++)
        {
            value += (int)(_upgrade.BaseCost * (_upgrade.CostMultiplier * (_level+i) + 1));
        }
        return value;
    }


    public void Buy(int index = 1)
    {
        _level+= index;
    }

}

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

    public UpgradeStat GetUpgradeStatByName(StatName statName) => _upgradeList.Find(x => x.Name == statName);
    public Upgrade GetUpgradeByName(StatName statName) => GetUpgradeStatByName(statName).GetUpgrade();
    
    public bool Buy(StatName statName, int multi = 1)
    {
        int cost = GetUpgradeStatByName(statName).GetCostFromMultiplier(multi);
        if (_coinAmount < cost) { return false; }
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
    
    public float Amount => _upgrade.BaseAmount + _level * _upgrade.BaseAmount;

    public float NextAmount => _upgrade.BaseAmount + (_level + 1) * _upgrade.BaseAmount;
    public float GetNextAmountFromMultiplier(int multi) => _upgrade.BaseAmount + (_level + multi) * _upgrade.BaseAmount;
    public int Cost => (int)(_upgrade.BaseCost * (0.2*_level + 1));

    public int GetCostFromMultiplier(int multi)
    {
        //Can be improved with maths
        int value = 0;
        for (int i = 0; i < multi; i++)
        {
            value += (int)(_upgrade.BaseCost * (0.2 * (_level+i) + 1));
        }
        return value;
    }


    public void Buy(int index = 1)
    {
        _level+= index;
    }

}

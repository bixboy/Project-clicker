using System;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeStat> _upgradeList;
    [SerializeField] private int _coinAmount;

    public void CollectCoin(int amount) => _coinAmount += amount;

    public UpgradeStat GetUpgradeStatByName(StatName statName) => _upgradeList.Find(x => x.Name == statName);
    public Upgrade GetUpgradeByName(StatName statName) => GetUpgradeStatByName(statName).GetUpgrade();
    
    public bool Buy(StatName statName)
    {
        if (_coinAmount < GetUpgradeStatByName(statName).Cost) { return false; }
        _coinAmount -= GetUpgradeStatByName(statName).Cost;
        GetUpgradeStatByName(statName).Buy();
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
    public int Cost => (int)(_upgrade.BaseCost * (0.2*_level + 1));

    public void Buy()
    {
        _level++;
    }

}

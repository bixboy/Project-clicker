using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField, Expandable] List<Upgrade> _upgradeList;
    [SerializeField] private int _coinAmount;

    public void CollectCoin(int amount) => _coinAmount += amount;

    public Upgrade GetUpgradeByName(StatName statName) => _upgradeList.Find(x => x.Name == statName);
    
    public bool Buy(StatName statName)
    {
        if (_coinAmount < GetUpgradeByName(statName).Cost) { return false; }
        _coinAmount -= GetUpgradeByName(statName).Cost;
        GetUpgradeByName(statName).Buy();
        return true;
    }
}

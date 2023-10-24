using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private StatName _displayedUpgrade;
    [Header("ObjectToReference")]
    [SerializeField] private TMP_Text _upgradeName;
    [SerializeField] private TMP_Text _upgradeCost;
    [SerializeField] private TMP_Text _currentAmount;
    [SerializeField] private TMP_Text _nextAmount;
    [SerializeField] private Image _upgradeSprite;
    private UpgradeManager _upgradeManager;
    private Upgrade _upgrade;

    private void Awake()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        UpdateUI();
    }
    private void UpdateUI()
    {
        _upgrade = _upgradeManager.GetUpgradeByName(_displayedUpgrade);
        _upgradeName.text = _displayedUpgrade.ToString();
        _upgradeCost.text = _upgrade.Cost.ToString();
        _currentAmount.text = _upgrade.Amount.ToString(CultureInfo.InvariantCulture);
        _nextAmount.text = _upgrade.NextAmount.ToString(CultureInfo.InvariantCulture);
        _upgradeSprite.sprite = _upgrade.Sprite;
    }

    public void Buy(StatName statName)
    {
        _upgradeManager.Buy(statName);
        UpdateUI();
    }


}

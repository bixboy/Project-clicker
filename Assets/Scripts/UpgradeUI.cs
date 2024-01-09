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
    [SerializeField] private Dropdown _dropdown;
    private UpgradeStat _upgrade;

    private void Awake()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        UpdateUI();
        _dropdown.onValueChanged += UpdateUI;
    }
    private void UpdateUI()
    {
        int multi = _dropdown.GetMultiplier();
        _upgrade = _upgradeManager.GetUpgradeStatByName(_displayedUpgrade);
        _upgradeName.text = _upgrade.GetUpgrade().StringName;
        _upgradeCost.text = _upgrade.GetCostFromMultiplier(multi).ToString();
        _currentAmount.text = _upgrade.Amount.ToString(CultureInfo.InvariantCulture);
        _nextAmount.text = _upgrade.GetNextAmountFromMultiplier(multi).ToString(CultureInfo.InvariantCulture);
        _upgradeSprite.sprite = _upgrade.GetUpgrade().Sprite;
    }

    public void Buy()
    {
        _upgradeManager.Buy(_displayedUpgrade, _dropdown.GetMultiplier());
        UpdateUI();
    }


}

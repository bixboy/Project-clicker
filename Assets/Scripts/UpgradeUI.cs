using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UsefulScript;

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

    private void Start()
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
        _upgradeCost.text = Scripts.NumberToString(_upgrade.GetCostFromMultiplier(multi), 3, 2);
        _currentAmount.text =_displayedUpgrade==StatName.AttackSpeed? _upgrade.Amount.ToString() :  Scripts.NumberToString((int)_upgrade.Amount,3,1);
        _nextAmount.text = _displayedUpgrade==StatName.AttackSpeed? _upgrade.GetNextAmountFromMultiplier(multi).ToString() : Scripts.NumberToString((int) _upgrade.GetNextAmountFromMultiplier(multi),3,1);
        _upgradeSprite.sprite = _upgrade.GetUpgrade().Sprite; 
    }

    public void Buy()
    {
        _upgradeManager.Buy(_displayedUpgrade, _dropdown.GetMultiplier());
        UpdateUI();
    }


}

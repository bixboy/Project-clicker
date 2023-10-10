using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private StatName _displayedUpgrade;
    [Header("ObjectToReference")]
    [SerializeField] private TMP_Text _upgradeName;
    [SerializeField] private TMP_Text _upgradeCost;
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
        _upgradeSprite.sprite = _upgrade.Sprite;
    }


}

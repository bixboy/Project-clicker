using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UsefulScript;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settings;

    [SerializeField]
    private Image _spriteRenderer;
    [SerializeField]
    private Sprite _spriteRendererOpen;
    [SerializeField]
    private Sprite _spriteRendererClose;

    [SerializeField]
    private Slider _slider;

    private UpgradeManager _upgradeManager;
    [SerializeField] private TextMeshProUGUI _textGold;

    private void UpdateCoinUI(int coinAmount)
    {
        _textGold.text = Scripts.NumberToString(coinAmount, 6, 2);
    }

    private void Start()
    {
        _spriteRenderer.sprite = _spriteRendererClose;
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _upgradeManager.OnCoinValueChanged += UpdateCoinUI;
        _upgradeManager.CollectCoin(0);
    }

    public void SetEnemyCount(int count)
    {
        _slider.value = 0;
        _slider.maxValue = count;
    }

    public void AddCurrentEnemyCount()
    {
        _slider.value += 1;
    }

    public void OpenSettings()
    {
        _settings.SetActive(true);
        _spriteRenderer.sprite = _spriteRendererOpen;
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        _spriteRenderer.sprite = _spriteRendererClose;
    }
}

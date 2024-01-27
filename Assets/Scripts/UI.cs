using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UsefulScript;

public class UI : MonoBehaviour
{
    [SerializeField, Foldout("Settings")] private GameObject _settings;
    [SerializeField, Foldout("Settings")] private Image _spriteRenderer;
    [SerializeField, Foldout("Settings")] private Sprite _spriteRendererOpen;
    [SerializeField, Foldout("Settings")] private Sprite _spriteRendererClose;

    [SerializeField, Foldout("Level")] private GameObject _pageLevel;
    [SerializeField, Foldout("Level")] private TextMeshProUGUI _textLvl;
    [SerializeField, Foldout("Level")] private List<LevelChoiceUI> _levelChoices;

    [SerializeField, Foldout("Upgrades")] private GameObject _pageUpgrade;

    [SerializeField, Foldout("Skills")] private GameObject _pageSkills;
    [SerializeField, Foldout("Skills")] private SkillPageInfos _skillPageInfo;

    [SerializeField, Foldout("Boss")] private Slider _slider;
    [SerializeField, Foldout("Boss")] private Image _spriteBossBar;
    [SerializeField, Foldout("Boss")] private Sprite _imageBoss;
    [SerializeField, Foldout("Boss")] private transition _animeBoss;

    [SerializeField, Foldout("Timer")] private Slider _sliderTime;
    [SerializeField, Foldout("Timer")] private GameObject _timer;
    [SerializeField, Foldout("Timer")] private int _maxTime;
    [SerializeField, Foldout("Timer")] private Animator _animator;

    [SerializeField] private TextMeshProUGUI _textGold;
    private UpgradeManager _upgradeManager;
    private EnemyLife _bossLife;
    private LevelManager _levelManager;

    public void SetEnemyLife(EnemyLife bossLife) => _bossLife = bossLife;

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
        _levelManager = _upgradeManager.GetComponent<LevelManager>();
        _textLvl.text = "Level " + _levelManager.GetLevelLoaded();
        foreach (LevelChoiceUI levelChoiceUI in _levelChoices)
        {
            levelChoiceUI.SetUI(_levelManager);
        }
    }

    public void SetSkill(SkillName skillName)
    {
        _skillPageInfo.SetSkill(skillName);
    }

    public void SetEnemyCount(int count)
    {
        _slider.value = 0;
        _slider.maxValue = count;
    }

    public void AddCurrentEnemyCount()
    {
        if (_slider.value <= _slider.maxValue)
        {
            _slider.value += 1;
            CheckAllEnemyDie();
        }
    }

    private void Update()
    {
        if (_sliderTime.maxValue == _maxTime && _sliderTime.value >= 0)
        {
            _sliderTime.value -= Time.deltaTime;
            if (_sliderTime.value <= 0 )
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void CheckAllEnemyDie()
    {
        if (_slider.value >= _slider.maxValue)
        {
            _spriteBossBar.sprite = _imageBoss;
            _slider.maxValue = _bossLife.CurrentHealth;
            _slider.value = _slider.maxValue;
            _slider.GetComponentInChildren<Image>().color = new Color32(181, 22, 0, 255);
            _animator.enabled = true;
            _timer.SetActive(true);
            _sliderTime.maxValue = _maxTime;
            _sliderTime.value = _slider.maxValue;
            _animeBoss.SetUiTransitionBoss(_bossLife.GetComponent<SpriteRenderer>().sprite, _bossLife.name, _levelManager.GetLevelLoaded());
        }
    }

    public void OpenUpgrade()
    {
        _pageSkills.SetActive(false);
        _pageUpgrade.SetActive(true);
    }

    public void OpenSkills()
    {
        _pageUpgrade.SetActive(false);
        _pageSkills.SetActive(true);
    }


    public void openLvl()
    {
        _pageLevel.SetActive(true);
    }

    public void closeLvl()
    {
        _pageLevel.SetActive(false);
    }

    public void openSettings()
    {
        _settings.SetActive(true);
        _spriteRenderer.sprite = _spriteRendererOpen;
    }

    public void closeSettings()
    {
        _settings.SetActive(false);
        _spriteRenderer.sprite = _spriteRendererClose;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UsefulScript;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _pageLevel;
    [SerializeField] private GameObject _pageUpgrade;
    [SerializeField] private GameObject _pageSkills;

    [SerializeField]
    private Image _spriteRenderer;
    [SerializeField]
    private Sprite _spriteRendererOpen;
    [SerializeField]
    private Sprite _spriteRendererClose;

    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Image _spriteBossBar;
    [SerializeField]
    private Sprite _imageBoss;
    EnemyLife _bossLife;

    [SerializeField] private Slider _sliderTime;
    [SerializeField] private GameObject _timer;
    [SerializeField] private int _maxTime;
    [SerializeField] private Animator _animator;

    private UpgradeManager _upgradeManager;
    [SerializeField] private TextMeshProUGUI _textGold;
    [SerializeField] private SkillPageInfos _skillPageInfo;

    [SerializeField] private TextMeshProUGUI _textLvl;
    [SerializeField] private List<LevelChoiceUI> _levelChoices;

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
        LevelManager levelManager = _upgradeManager.GetComponent<LevelManager>();
        _textLvl.text = "Level " + levelManager.GetLevelLoaded();
        foreach (LevelChoiceUI levelChoiceUI in _levelChoices)
        {
            levelChoiceUI.SetUI(levelManager);
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

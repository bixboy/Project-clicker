using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UsefulScript;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _pageLevel;

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

    private UpgradeManager _upgradeManager;
    [SerializeField] private TextMeshProUGUI _textGold;

    private Dictionary<string, GameObject> _pages = new Dictionary<string, GameObject>();
    private GameObject currentPage;
    [SerializeField] private string pagePrincipale;

    private GameObject _skills;

    public void SetEnemyLife(EnemyLife bossLife) => _bossLife = bossLife;

    private void UpdateCoinUI(int coinAmount)
    {
        _textGold.text = Scripts.NumberToString(coinAmount, 6, 2);
    }

    private void Start()
    {
        OpenPage(pagePrincipale);
        _spriteRenderer.sprite = _spriteRendererClose;
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _upgradeManager.OnCoinValueChanged += UpdateCoinUI;
        _upgradeManager.CollectCoin(0);
    }

    public void SetSkill(GameObject skill)
    {
        _skills = skill;
    }

    public GameObject GetSkill() { return _skills; }

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

    private void CheckAllEnemyDie()
    {
        if (_slider.value >= _slider.maxValue)
        {
            _spriteBossBar.sprite = _imageBoss;
            _slider.maxValue = _bossLife.CurrentHealth;
            _slider.value = _slider.maxValue;
            _slider.GetComponentInChildren<Image>().color = new Color32(181, 22, 0, 255);
        }
    }

    public void RegisterPage(string pageName, GameObject pageObject)
    {
        if (!_pages.ContainsKey(pageName))
        {
            _pages.Add(pageName, pageObject);
            pageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("La page avec le nom '" + pageName + "' est déjà enregistrée.");
        }
    }

    public void OpenPage(string pageName)
    {
        CloseCurrentPage(pageName);

        if (_pages.TryGetValue(pageName, out GameObject newPage))
        {
            newPage.SetActive(true);
            currentPage = newPage;
        }
        else
        {
            Debug.LogError("La page avec le nom '" + pageName + "' n'a pas été trouvée.");
        }
    }

    public void CloseCurrentPage(string pageName)
    {
        // Fermer la page actuelle si elle est définie
        if (currentPage != null && currentPage)
        {
            currentPage.SetActive(false);
            currentPage = null;
        }
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

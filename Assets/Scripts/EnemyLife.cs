using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{

    // Field
    [SerializeField, ValidateInput("ValidateMaxHealth")]
    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private bool _isDie;
    private bool _isDieFirst = true;
    public bool IsDie => _isDie;

    [SerializeField] private GameObject _gameobjectCoins;
    [SerializeField] private UnityEvent _onDamage;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeDisabledLifeBar;
    private float currentCountdown;

    private Animator _animator;
    public event Action OnDeath;

    private UpgradeManager _upgradeManager;

    // Properties
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }


    // Methodes
    #region EditorParametre

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        if (!CompareTag("Boss"))
        {
            _canvas = GetComponentInChildren<Canvas>();
            _slider = _canvas.GetComponentInChildren<Slider>();
            _slider.maxValue = _maxHealth;
        }
        else
        {
            _slider = GameObject.FindWithTag("UiSlider").GetComponent<Slider>();
        }
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _maxHealth = 100;
    }

    private bool ValidateMaxHealth()
    {
        // Guards
        if (_maxHealth <= 0)
        {
            _maxHealth = 100;
            Debug.LogWarning("Pas de HPMax négatif");
            return false;
        }
        return true;
    }

    #endregion

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void SetManager(UpgradeManager upgradeManager) => _upgradeManager = upgradeManager;

    private void Regen(int amount)
    {
        // Guards
        if (amount < 0)
        {
            throw new ArgumentException("Mauvaise valeur, valeur négative");
        }

        if (_isDie)
        {
            return;
        }

        _currentHealth += amount;

        _currentHealth = Math.Clamp(_currentHealth + amount, 0, _maxHealth);

        Debug.Log("Heal");
    }

    public void TakeDamage(int amount)
    {
        // Guards
        if (amount < 0)
        {
            throw new ArgumentException("Mauvaise valeur, valeur négative");
        }

        // _currentHealth -= amount;

        _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);

        _canvas.enabled = true;
        _slider.value = _currentHealth;

        if (_currentHealth <= 0) Die();
        DisabledLifeBar();
        _onDamage.Invoke();

    }

    private void DisabledLifeBar()
    {
        currentCountdown = _timeDisabledLifeBar;
    }

    private void Update()
    {
        currentCountdown -= Time.deltaTime;
        if (currentCountdown <= 0f) 
        {
            _canvas.enabled = false;
        }
    }

    private void Die()
    {
        _isDie = true;
        _currentHealth = 0;
        int nbCoinDropped = UnityEngine.Random.Range(3, 5);
        int coinValue = _upgradeManager.GetComponent<LevelManager>().GetLevelLoaded();

        GameObject[] soldats = GameObject.FindGameObjectsWithTag("Soldiers");
        foreach (GameObject soldat in soldats)
        {
            soldat.GetComponent<MoveSoldats>().SetDestinationActif(false);
        }
        Debug.Log("Die");
        if(_isDieFirst)
        {
            for (int i = 0; i < nbCoinDropped; i++)
            {
                Coins newCoin = Instantiate(_gameobjectCoins, GetComponent<Transform>().position, GetComponent<Transform>().rotation).GetComponent<Coins>();
                newCoin.SetStats(_upgradeManager, coinValue);
            }
            _isDieFirst = false;
            _animator.SetTrigger("Die");
        }
    }

    private void destroyEnemy()
    {
        Debug.Log("Enemy Die");
        Destroy(gameObject);
        OnDeath?.Invoke();
    }

    [Button]
    private void TakeDamage() => TakeDamage(10);
    [Button]
    private void Regen() => Regen(5);

}

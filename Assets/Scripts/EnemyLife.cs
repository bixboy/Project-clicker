using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private Animator _animator;

    private UpgradeManager _upgradeManager;
    // Properties
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }


    // Methodes
    #region EditorParametre

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
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

    public void SetMaxHealth(int maxHealth) => _maxHealth = maxHealth;

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

        if (_currentHealth <= 0) Die();

        _onDamage.Invoke();

    }

    private void Die()
    {
        _isDie = true;
        _currentHealth = 0;
        int value = UnityEngine.Random.Range(3, 5);

        GameObject[] soldats = GameObject.FindGameObjectsWithTag("Soldiers");
        foreach (GameObject soldat in soldats)
        {
            soldat.GetComponent<MoveSoldats>().SetDestinationActif(false);
        }

        Debug.Log("Die");
        if(_isDieFirst)
        {
            for (int i = 0; i < value; i++)
            {
                GameObject newCoin = Instantiate(_gameobjectCoins, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
                newCoin.GetComponent<Coins>().SetManager(_upgradeManager);
            }
            _isDieFirst = false;
            _animator.SetTrigger("Die");
        }
    }

    private void destroyEnemy()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

    [Button]
    private void coucou() => TakeDamage(10);
    [Button]
    private void coucou2() => Regen(5);

}

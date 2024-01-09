using NaughtyAttributes;
using spawn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldiersLife : MonoBehaviour
{
    // Field
    [SerializeField, ValidateInput("ValidateMaxHealth")]
    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private bool _isDie;
    private UpgradeManager _upgradeManager;

    [SerializeField] private UnityEvent _onDamage;

    // Methodes
    #region EditorParametre

    private void Awake()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _maxHealth = (int)_upgradeManager.GetUpgradeStatByName(StatName.Health).Amount;
        _currentHealth = _maxHealth;
        
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _maxHealth = 100;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    private bool ValidateMaxHealth()
    {
        // Guards
        if (_maxHealth <= 0)
        {
            _maxHealth = 100;
            Debug.LogWarning("Pas de HPMax n�gatif");
            return false;
        }
        return true;
    }

    #endregion


    private void Regen(int amount)
    {
        // Guards
        if (amount < 0)
        {
            throw new ArgumentException("Mauvaise valeur, valeur n�gative");
        }

        if (_isDie)
        {
            return;
        }
        _currentHealth = Math.Clamp(_currentHealth + amount, 0, _maxHealth);

        Debug.Log("Heal");
    }

    public bool TakeDamage(int amount)
    {
        // Guards
        if (amount < 0)
        {
            throw new ArgumentException("Mauvaise valeur, valeur n�gative");
        }

        // _currentHealth -= amount;

        _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);

        _onDamage.Invoke();
        Debug.Log("Damage");

        if (_currentHealth <= 0) { Die(); return true; }
        return false;

    }

    private void Die()
    {
        _isDie = true;
        _currentHealth = 0;

        InvokeSoldats invoke = FindObjectOfType<InvokeSoldats>();
        invoke.RemoveSoldierFromList(this);

        Debug.Log("Die");

        destroySoldat();
    }

    private void destroySoldat()
    {
        Destroy(gameObject);
    }

    [Button]
    private void coucou() => TakeDamage(10);
    [Button]
    private void coucou2() => Regen(5);
}

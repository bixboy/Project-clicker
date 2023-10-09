using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLife : MonoBehaviour
{

    // Field
    [SerializeField, ValidateInput("ValidateMaxHealth")] int _maxHealth;
    [SerializeField] int _currentHealth;

    bool _isDie;

    [SerializeField] UnityEvent _onDamage;

    // Properties
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }


    // Methodes
    #region EditorParametre

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _maxHealth = 100;
    }

    bool ValidateMaxHealth()
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


    void Regen(int amount)
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

        Debug.Log("Damage");
    }

    void Die()
    {
        _isDie = true;
        _currentHealth = 0;

        var soldats = GameObject.FindGameObjectWithTag("Soldiers");
        soldats.GetComponent<MoveSoldats>()._destActif = false;

        Debug.Log("Die");

        destroyEnemy();
    }

    void destroyEnemy()
    {
        Destroy(gameObject);
    }

    [Button] void coucou() => TakeDamage(10);
    [Button] void coucou2() => Regen(5);

}

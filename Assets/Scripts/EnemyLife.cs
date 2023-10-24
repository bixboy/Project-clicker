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

    [SerializeField] private UnityEvent _onDamage;
    private Animator _animator;

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

        Debug.Log("Damage");
    }

    private void Die()
    {
        _isDie = true;
        _currentHealth = 0;

        GameObject[] soldats = GameObject.FindGameObjectsWithTag("Soldiers");
        foreach (GameObject soldat in soldats)
        {
            soldat.GetComponent<MoveSoldats>().SetDestinationActif(false);
        }

        Debug.Log("Die");
        _animator.SetTrigger("Die");
    }

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }

    [Button]
    private void coucou() => TakeDamage(10);
    [Button]
    private void coucou2() => Regen(5);

}

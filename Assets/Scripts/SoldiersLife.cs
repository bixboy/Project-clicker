using NaughtyAttributes;
using spawn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class SoldiersLife : MonoBehaviour
{
    // Field
    [SerializeField, ValidateInput("ValidateMaxHealth")]
    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] public bool _isDead;
    private UpgradeManager _upgradeManager;

    [SerializeField] private float _TimeDieClear;
    private float _currentCountdown;
    private bool _isDieFirstTime = false;

    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private Animator _animator;
    
    private InvokeSoldats _invoker;
    public void SetInvoker(InvokeSoldats invoker) => _invoker = invoker;

    // Methodes
    #region EditorParametre

    private void Start()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _maxHealth = (int)_upgradeManager.GetUpgradeStatByName(StatName.Health).Amount;
        _currentHealth = _maxHealth;
        _animator = gameObject.GetComponent<Animator>();
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

        if (_isDead)
        {
            return;
        }
        _currentHealth = Math.Clamp(_currentHealth + amount, 0, _maxHealth);

        Debug.Log("Heal");
    }

    public bool TakeDamage(int amount)
    {
        if (_invoker.isSkillActive(SkillName.Necromancier)) return false;
        // Guards
        if (amount < 0)
        {
            throw new ArgumentException("Mauvaise valeur, valeur n�gative");
        }

        // _currentHealth -= amount;

        _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);

        if (_currentHealth>0 || !_isDieFirstTime) _onDamage.Invoke();

        if (_currentHealth <= 0 && !_isDieFirstTime) { _currentCountdown = _TimeDieClear; DieAnim(); return true; }
        return false;

    }

    private void Update()
    {
        if (_isDead) 
        {
            _currentCountdown -= Time.deltaTime;
            if (_currentCountdown <= 0f)
            {
                Die();
            }
        }
    }

    private void DieAnim()
    {
        _isDead = true;
        _isDieFirstTime = true;

        InvokeSoldats invoke = FindObjectOfType<InvokeSoldats>();
        invoke.RemoveSoldierFromList(this);

        _animator.SetTrigger("Die");
    }

    private void Die()
    {
        _currentHealth = 0;

        Debug.Log("Die");

        destroySoldat();
    }

    private void destroySoldat()
    {
        Destroy(gameObject);
    }

    [Button]
    private void TakeDamage() => TakeDamage(10);
    [Button]
    private void Regen() => Regen(5);
}

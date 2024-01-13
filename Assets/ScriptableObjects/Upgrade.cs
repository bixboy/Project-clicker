using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private StatName _statName;
    [SerializeField] private String _stringName;
    [SerializeField] private float _startAmount;
    [SerializeField] private bool _hasMaxAmount;
    [SerializeField, ShowIf("_hasMaxAmount")] private float _maxAmount;
    [SerializeField] private float _amount;
    [SerializeField] private int _cost;
    [SerializeField] private float _costMultiplier;
    [SerializeField] private Sprite _sprite;

    public StatName Name => _statName;
    public String StringName => _stringName;
    public int BaseCost => _cost;

    public float MaxAmount => _hasMaxAmount ? _maxAmount : Mathf.Infinity;
    public float StartAmount => _startAmount;
    public float Amount => _amount;
    public Sprite Sprite => _sprite;

    
}

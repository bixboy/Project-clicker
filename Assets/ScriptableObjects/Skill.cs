using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades")]
public class Skill : ScriptableObject
{
    [SerializeField] private SkillName _skillName;
    [SerializeField] private String _stringName;
    [SerializeField] private int _unlockLevel;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _startAmount;
    [SerializeField] private bool _hasMaxAmount;
    [SerializeField, ShowIf("_hasMaxAmount")] private float _maxAmount;
    [SerializeField] private float _amount;
    [SerializeField] private int _cost;
    [SerializeField] private float _costMultiplier;
    [SerializeField] private Sprite _sprite;

    public SkillName Name => _skillName;
    public String StringName => _stringName;
    public int BaseCost => _cost;

    public int UnlockLevel => _unlockLevel;
    public float Cooldown => _cooldown; 
    public float CostMultiplier => _costMultiplier;

    public float MaxAmount => _hasMaxAmount ? _maxAmount : Mathf.Infinity;
    public float StartAmount => _startAmount;
    public float Amount => _amount;
    public Sprite Sprite => _sprite;

    
}

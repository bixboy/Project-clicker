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
    [SerializeField, TextArea] private String _description;
    [SerializeField] private int _unlockLevel;
    [SerializeField] private int _cooldown;
    [SerializeField] private int _cost;
    [SerializeField] private float _costMultiplier;
    [SerializeField] private Sprite _sprite;

    public SkillName Name => _skillName;
    public String StringName => _stringName;

    public String Description => _description;
    public int BaseCost => _cost;

    public int UnlockLevel => _unlockLevel;
    public int Cooldown => _cooldown; 
    public float CostMultiplier => _costMultiplier;
    public Sprite Sprite => _sprite;

    
}

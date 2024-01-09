using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private StatName _statName;
    [SerializeField] private String _stringName;
    [SerializeField] private float _amount;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _sprite;

    public StatName Name => _statName;
    public String StringName => _stringName;
    public int BaseCost => _cost;
    
    public float BaseAmount => _amount;
    public Sprite Sprite => _sprite;

    
}

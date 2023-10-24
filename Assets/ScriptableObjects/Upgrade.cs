using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private StatName _statName;
    [SerializeField] private float _amount;
    [SerializeField] private int _level;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _sprite;

    public StatName Name => _statName;
    public float Amount => _amount + _level * _amount;

    public float NextAmount => _amount + (_level + 1) * _amount;
    public int Level => _level;
    public float BaseAmount => _amount;
    public int Cost => (int)(_cost * (0.2*_level + 1));
    public Sprite Sprite => _sprite;
    public void Buy()
    {
        _level++;
    }

}

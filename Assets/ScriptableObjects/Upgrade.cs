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
    public float Amount => _amount;
    public int Level => _level;
    public float value => _level * _amount;
    public int Cost => (int)(_cost * (0.2*_level + 1));
    public Sprite Sprite => _sprite;
    public void Buy()
    {
        _level++;
    }

}

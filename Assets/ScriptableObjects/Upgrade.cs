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
    public int Cost => _cost;
    public Sprite Sprite => _sprite;
    public void Buy()
    {
        _cost *= 2;
        _level++;
    }

}

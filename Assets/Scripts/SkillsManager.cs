using System;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UsefulScript;

public class SkillsManager : MonoBehaviour
{
    [SerializeField] private List<SkillStat> _skillsList;
    private UpgradeManager _upgradeManager;

    private void Awake()
    {
        _upgradeManager = gameObject.GetComponent<UpgradeManager>();
    }

    public SkillStat GetSkillStatByName(SkillName skillName) => _skillsList.Find(x => x.Name == skillName);
    public Skill GetSkillByName(SkillName skillName) => GetSkillStatByName(skillName).GetSkill();
    
    public bool Buy(SkillName skillName, int multi = 1)
    {
        int cost = GetSkillStatByName(skillName).GetCostFromMultiplier(multi);
        if (_upgradeManager.GetCoinAmount() < cost || GetSkillStatByName(skillName).MaxAmountReached) { return false; }
        _upgradeManager.RemoveCoin(cost);
        GetSkillStatByName(skillName).Buy(multi);
        return true;
    }

    public void Use(SkillName skillName)
    {
        SkillStat skill = GetSkillStatByName(skillName);
        if (!skill.CanBeUsed() || !skill.IsUnlocked()) return;
        skill.Use();
        switch (skillName)
        {
            case SkillName.Cats:
                //Launch Cats
                break;
            case SkillName.NinjaSoldier:
                //Launch NinjaSoldier
                break;
            case SkillName.Necromancier:
                //Launch Necromancier
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(skillName), skillName, null);
        }
    }
}

[Serializable]
public class SkillStat
{
    [SerializeField, Expandable] private Skill _skill;
    [SerializeField] private int _level;
    [SerializeField] private bool _unlocked;
    private float _lastTimeUsed;

    public Skill GetSkill() => _skill;
    public int GetLevel() => _level;
    public SkillName Name => _skill.Name;

    public bool IsUnlocked() => _unlocked;

    public bool CanBeUsed() => _unlocked && _lastTimeUsed <= Time.time + _skill.Cooldown;

    public float Progress() => Mathf.Clamp((_lastTimeUsed - Time.time) / _skill.Cooldown ,0,1);

    public void Use() => _lastTimeUsed = Time.time;
    
    public float Amount => Mathf.Min(_skill.StartAmount + _level * _skill.Amount, _skill.MaxAmount);

    public float NextAmount => Mathf.Min(_skill.StartAmount + (_level + 1) * _skill.Amount, _skill.MaxAmount);
    public float GetNextAmountFromMultiplier(int multi) => _skill.StartAmount + (_level + multi) * _skill.Amount;
    public int Cost => (int)(_skill.BaseCost * (_skill.CostMultiplier*_level + 1));

    public bool MaxAmountReached => Amount == NextAmount;
    
    public int GetCostFromMultiplier(int multi)
    {
        //Can be improved with maths
        int value = 0;
        for (int i = 0; i < multi; i++)
        {
            value += (int)(_skill.BaseCost * (_skill.CostMultiplier * (_level+i) + 1));
        }
        return value;
    }


    public void Buy(int index = 1)
    {
        if (!_unlocked)
        {
            _lastTimeUsed = 0;
            _unlocked = true;
            return;
        }
        _level+= index;
    }

}

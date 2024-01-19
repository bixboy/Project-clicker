using System;
using NaughtyAttributes;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillsManager : MonoBehaviour
{
    [SerializeField] private List<SkillStat> _skillsList;
    [SerializeField] private List<GameObject> _skillPrefab;
    private UpgradeManager _upgradeManager;
    private CinemachineVirtualCamera _camera;
    [Header("EditorTest")]
    [SerializeField] private SkillName _skillNameEditor;
    [Button] private void UseSkill() => Use(_skillNameEditor);
    

    private void Awake()
    {
        _upgradeManager = gameObject.GetComponent<UpgradeManager>();
        _camera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        SceneManager.sceneLoaded += SetCamera;
    }
    public void SetCamera(Scene scene, LoadSceneMode sceneMode) => _camera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    public SkillStat GetSkillStatByName(SkillName skillName) => _skillsList.Find(x => x.Name == skillName);
    public Skill GetSkillByName(SkillName skillName) => GetSkillStatByName(skillName).GetSkill();
    
    public bool Buy(SkillName skillName)
    {
        int cost = GetSkillStatByName(skillName).Cost;
        if (_upgradeManager.GetCoinAmount() < cost || GetSkillStatByName(skillName).MaxAmountReached) { return false; }
        _upgradeManager.RemoveCoin(cost);
        GetSkillStatByName(skillName).Buy();
        return true;
    }

    public void Use(SkillName skillName)
    {
        SkillStat skill = GetSkillStatByName(skillName);
        if (!skill.CanBeUsed() || !skill.IsUnlocked()) return;
        skill.Use();
        ISkill skillObj = null;
        switch (skillName)
        {
            case SkillName.Cats:
                Debug.Log(_camera);
                CatMovement cat = Instantiate(_skillPrefab[0], _camera.transform.position + new Vector3(-10,4,0), transform.rotation).GetComponent<CatMovement>();
                cat.SetCamera(_camera);
                skillObj = cat;
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
        if (skillObj!=null) skillObj.SetStat(skill.GetLevel());
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


    public void Buy()
    {
        if (!_unlocked)
        {
            _lastTimeUsed = 0;
            _unlocked = true;
            return;
        }
        _level+= 1;
    }

}

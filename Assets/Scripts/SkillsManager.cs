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

    public event Action<ISkill> skillSummoned;
    

    private void Awake()
    {
        _upgradeManager = gameObject.GetComponent<UpgradeManager>();
        GameObject cam = GameObject.FindWithTag("VirtualCamera");
        if (cam!=null) _camera = cam.GetComponent<CinemachineVirtualCamera>();
        SceneManager.sceneLoaded += SetCamera;
    }
    public void SetCamera(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject cam = GameObject.FindWithTag("VirtualCamera");
        if (cam!=null) _camera = cam.GetComponent<CinemachineVirtualCamera>();
    }
    public SkillStat GetSkillStatByName(SkillName skillName) => _skillsList.Find(x => x.Name == skillName);
    public Skill GetSkillByName(SkillName skillName) => GetSkillStatByName(skillName).GetSkill();
    
    public bool Buy(SkillName skillName)
    {
        int cost = GetSkillStatByName(skillName).Cost;
        if (_upgradeManager.GetCoinAmount() < cost) { return false; }
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
                CatMovement cat = Instantiate(_skillPrefab[0], _camera.transform.position + new Vector3(-10,4,10), transform.rotation).GetComponent<CatMovement>();
                cat.SetCamera(_camera);
                skillObj = cat;
                break;
            case SkillName.NinjaSoldier:
                skillObj = Instantiate(_skillPrefab[1], _camera.transform.position + new Vector3(-10,5,10), transform.rotation).GetComponent<ISkill>();
                break;
            case SkillName.Necromancier:
                Necromancer necromancer = Instantiate(_skillPrefab[2], _camera.transform.position + new Vector3(-10,10,10), transform.rotation).GetComponent<Necromancer>();
                necromancer.SetCam(_camera);
                skillObj = necromancer;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(skillName), skillName, null);
        }
        if (skillObj!=null) skillObj.SetStat(skill.GetLevel());
        skillSummoned?.Invoke(skillObj);
    }
}

[Serializable]
public class SkillStat
{
    [SerializeField, Expandable] private Skill _skill;
    [SerializeField] private int _level;
    [SerializeField] private bool _unlocked;
    [SerializeField] private float _lastTimeUsed;

    public Skill GetSkill() => _skill;
    public int GetLevel() => _level;
    public SkillName Name => _skill.Name;

    public bool IsUnlocked() => _unlocked;

    public bool CanBeUsed() => _lastTimeUsed + _skill.Cooldown <= Time.time;

    public float Progress() => Mathf.Clamp((_lastTimeUsed + _skill.Cooldown - Time.time) / _skill.Cooldown ,0,1);

    public void Use() => _lastTimeUsed = Time.time;
    public int Cost => (int)(_skill.BaseCost * (_skill.CostMultiplier*_level + 1));


    public void Buy()
    {
        if (!_unlocked)
        {
            _lastTimeUsed = -_skill.Cooldown;
            _unlocked = true;
            return;
        }
        _level+= 1;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UsefulScript;

public class SkillPageInfos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _locked;
    private SkillsManager _skillsManager;
    private Skill _skill;

    public void SetSkill(SkillName skillName)
    {
        if (_skillsManager==null) _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();
        _skill = _skillsManager.GetSkillByName(skillName);
        _icon.sprite = _skill.Sprite;
        _skillNameText.text = _skill.StringName;
        _descriptionText.text = _skill.Description;
        _cooldownText.text = Scripts.GetTimeString(_skill.Cooldown);
        _levelText.text = "Niv " + _skillsManager.GetSkillStatByName(skillName).GetLevel();
        _locked.SetActive(!_skillsManager.GetSkillStatByName(skillName).IsUnlocked());
    }

    public void Buy()
    {
        _skillsManager.GetSkillStatByName(_skill.Name).Buy();
        if (_locked.activeSelf) _locked.SetActive(false);
    }
}

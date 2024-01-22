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
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _locked;
    private GameObject _skillObject;
    private SkillsManager _skillsManager;
    private Skill _skill;
    [SerializeField] private SkillName _skillName;

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
        _skillName = skillName;
        _price.text = "Unlock : " + _skillsManager.GetSkillStatByName(skillName).Cost;
    }

    private void SetGameObject(GameObject gameObject)
    {
        _skillObject = gameObject;
    }

    public void equiped()
    {
        if (_skillsManager.GetSkillStatByName(_skill.Name).IsUnlocked())
        {
            foreach (var skills in GameObject.FindGameObjectsWithTag("skills"))
            {
                skills.GetComponent<SkillsButton>().SetSelected();
                skills.GetComponent<SkillsButton>().Choice(_skillName);
            };
            CloseInfoPage();
        }
    }

    public void Buy()
    {
        _skillsManager.Buy(_skillName);
        if (_skillsManager.GetSkillStatByName(_skillName).IsUnlocked()) _locked.SetActive(false);
    }

    public void CloseInfoPage()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

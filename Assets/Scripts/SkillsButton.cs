using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _back;
    private Image _skillIcon;
    private SkillName _skillName;
    private Skill _skill;
    private SkillsManager _skillsManager;
    private bool _selected;
    private bool _haveSkill;

    private void Start()
    {
        _gameObject.SetActive(false);
        _skillIcon = GetComponent<Image>();
        _selected = true;
        _haveSkill = false;
    }

    public void SetSelected() { _selected = false; }

    public void Choice(SkillName skillName)
    {
        _gameObject.SetActive(true);
        _back.SetActive(true);
        Debug.Log("ouiii");
        _skillName = skillName;
        Debug.Log(_skillName);
    }
    public void CloseAllButton()
    {
        foreach (var skills in GameObject.FindGameObjectsWithTag("skills"))
        {
            skills.GetComponent<SkillsButton>().Close();
        };
    }

    private void Close()
    {
        _selected = true;
        _gameObject.SetActive(false);
        _back.SetActive(false);
    }


    public void SetUIButton()
    {
        if (!_selected) 
        {
            if (_skillsManager == null) _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();
            _skill = _skillsManager.GetSkillByName(_skillName);
            _skillIcon.sprite = _skill.Sprite;
            _haveSkill = true;
            foreach (var skills in GameObject.FindGameObjectsWithTag("skills"))
            {
                skills.GetComponent<SkillsButton>().Close();
            };
        }
        else if(_haveSkill)
        {
            _skillsManager.Use(_skillName);
        }
    }
}
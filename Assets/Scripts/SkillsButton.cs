using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private Image _skillIcon;
    private SkillName _skillName;
    private Skill _skill;
    private SkillsManager _skillsManager;
    private bool _selected;

    private void Start()
    {
        _gameObject.SetActive(false);
        _skillIcon = GetComponent<Image>();
        _selected = true;
    }

    public void SetSelected() { _selected = false; }

    public void Choice(SkillName skillName)
    {
        _gameObject.SetActive(true);
        Debug.Log("ouiii");
        _skillName = skillName;
        Debug.Log(_skillName);
    }

    public void Close()
    {
        _selected = true;
        _gameObject.SetActive(false);
    }


    public void SetUIButton()
    {
        if (!_selected) 
        {
            if (_skillsManager == null) _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();
            _skill = _skillsManager.GetSkillByName(_skillName);
            _skillIcon.sprite = _skill.Sprite;
            foreach (var skills in GameObject.FindGameObjectsWithTag("skills"))
            {
                skills.GetComponent<SkillsButton>().Close();
            };
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InfoSkills : MonoBehaviour
{
    [SerializeField] private SkillName _skillName;
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private GameObject _page;
    [SerializeField] private UI _ui;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Image _imageSkill;

    private SkillStat _skillStat;
    private SkillsManager _skillsManager;
    private Skill _skill;
    private bool _change;

    private void Start()
    {
        _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();
        _skillStat = _skillsManager.GetSkillStatByName(_skillName);
        _skillNameText.text = _skillStat.GetSkill().StringName;
        _skill = _skillsManager.GetSkillByName(_skillName);
        _imageSkill.sprite = _skill.Sprite;
        _change = false;
    }

    private void Update()
    {
        if (_skillsManager.GetSkillStatByName(_skillName).IsUnlocked() && !_change)
        {
            Unlock(_skillName);
            _change = true;
        }
    }

    private void Unlock(SkillName skillName)
    {
        _lock.SetActive(false);
    }

    public void OpenInfoPage()
    {
        _ui.SetSkill(_skillName);
        _page.SetActive(true);
        _page.GetComponentInChildren<SkillPageInfos>().SetSkill(_skillName);
    }
}

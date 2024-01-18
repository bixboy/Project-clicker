using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class InfoSkills : MonoBehaviour
{
    [SerializeField] private SkillName _skillName;
    [SerializeField] private GameObject _page;
    [SerializeField] private GameObject _locked;
    [SerializeField] private UI _ui;
    [SerializeField] private Unlock _unlock;
    [SerializeField] private bool _isUnlocked;

    private SkillStat _skillStat;
    private SkillsManager _skillsManager;

    private string _skillname;
    private TextMeshProUGUI _textName;

    private void Start()
    {
        _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();

        _skillStat = _skillsManager.GetSkillStatByName(_skillName);
        _skillname = _skillStat.GetSkill().StringName;
        GetComponentInChildren<TextMeshProUGUI>().text = _skillname;
        _isUnlocked = _skillStat.IsUnlocked();
    }


    public void OpenInfoPage()
    {
        if (_isUnlocked)
        {
            OpenUnlocked();
        } else 
        {
            Debug.Log("Le skill '" + _skillname + "' n'est pas unlock");
            _ui.SetSkill(this.gameObject);
            OpenLocked();
        }
    }

    public void Unlock()
    {
        if (!_isUnlocked)
        {
            _skillsManager.Buy(_skillName);
            _isUnlocked = true;
            _locked.SetActive(false);
        }
    }

    public void equiped()
    {
        if (_isUnlocked)
        {
            CloseInfoPage();
            Debug.Log("Le skill '" + _skillname + "' est équiper");
        }
    }

    private void OpenLocked()
    {
        if (!_isUnlocked)
        {
            _page.SetActive(true);
            _textName = GameObject.Find("nameSkill").GetComponent<TextMeshProUGUI>();
            _textName.text = _skillname;
            _locked.SetActive(true);
        }
    }

    private void OpenUnlocked()
    {
        if (_isUnlocked) 
        {
            _page.SetActive(true);
            _locked.SetActive(false);
            _textName = GameObject.Find("nameSkill").GetComponent<TextMeshProUGUI>();
            _textName.text = _skillname;
        }
    }

    public void CloseInfoPage()
    {
        _page.SetActive(false);
    }
}

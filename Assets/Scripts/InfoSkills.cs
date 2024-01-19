using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class InfoSkills : MonoBehaviour
{
    [SerializeField] private SkillName _skillName;
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private GameObject _page;
    [SerializeField] private UI _ui;

    private SkillStat _skillStat;
    private SkillsManager _skillsManager;

    private void Start()
    {
        _skillsManager = GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>();
        _skillStat = _skillsManager.GetSkillStatByName(_skillName);
        _skillNameText.text = _skillStat.GetSkill().StringName;
    }


    public void OpenInfoPage()
    {
        _ui.SetSkill(_skillName);
        _page.SetActive(true); 
    }

    public void equiped()
    {
        if (_skillsManager.GetSkillStatByName(_skillName).IsUnlocked())
        {
            CloseInfoPage();
            Debug.Log("Le skill '" + _skillStat.GetSkill().StringName + "' est �quiper");
        }
    }


    public void CloseInfoPage()
    {
        _page.SetActive(false);
    }
}

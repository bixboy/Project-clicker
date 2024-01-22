using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkills : MonoBehaviour
{
    [SerializeField] private GameObject _uiSkillPage;
    [SerializeField] private Dictionary<SkillName, GameObject> _uiSkills = new Dictionary<SkillName, GameObject>();

    public GameObject GetSkills(SkillName skillName) 
    {
        if (_uiSkills.ContainsKey(skillName))
        {
            return _uiSkills[skillName];
        }
        else
        {
            Debug.LogWarning("La cl� '" + skillName + "' n'a pas �t� trouv�e dans le dictionnaire.");
            return null;
        }
    }
}

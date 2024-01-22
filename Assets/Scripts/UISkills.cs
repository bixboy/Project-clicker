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
            Debug.LogWarning("La clé '" + skillName + "' n'a pas été trouvée dans le dictionnaire.");
            return null;
        }
    }
}

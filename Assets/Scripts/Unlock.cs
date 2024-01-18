using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    [SerializeField] private UI _ui;
    GameObject _skill;

    public void Unlocked()
    {
        _skill = _ui.GetSkill();
        _skill.GetComponent<InfoSkills>().Unlock();
    }

}

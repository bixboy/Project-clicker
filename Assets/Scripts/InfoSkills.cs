using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoSkills : MonoBehaviour
{
    [SerializeField]
    private GameObject _page;
    [SerializeField]
    private string _name;

    private TextMeshProUGUI _textName;
    private bool _unlock = false;

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = _name;
    }

    public void OpenInfoPage()
    {
        if (_unlock)
        {
            _page.SetActive(true);
            _textName = GameObject.Find("nameSkill").GetComponent<TextMeshProUGUI>();
            _textName.text = _name;
        } else 
        {
            Debug.Log("Le skill '" + _name + "' n'est pas unlock");
        }
    }

    public void CloseInfoPage()
    {
        _page.SetActive(false);
    }
}

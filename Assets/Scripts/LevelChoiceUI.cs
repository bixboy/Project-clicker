using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject _locker;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private int _displayedLevel;
    private LevelManager _levelManager;
    private transition _transition;

    public void SetUI(LevelManager levelManager)
    {
        _transition = GameObject.FindWithTag("transition").GetComponent<transition>();
        _levelManager = levelManager;
        _levelText.text = "Level " + _displayedLevel;
        _locker.SetActive(levelManager.GetMaxLevelUnlocked()<_displayedLevel);
    }

    public void LoadLevel()
    {
        _transition.ChangeScene(_displayedLevel, _levelManager);
    }
}

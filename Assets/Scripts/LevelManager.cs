using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelLoaded;
    [SerializeField, Scene] private string _levelScene;
    [SerializeField] private AudioSource _audioSource;
    private int _maxLevelUnlocked;

    public int GetLevelLoaded() => _levelLoaded;
    public int GetMaxLevelUnlocked() => _maxLevelUnlocked;

    private void Awake()
    {
        _maxLevelUnlocked = 1;
        _levelLoaded = 1;
        SceneManager.sceneLoaded += LaunchSound;
    }

    private void LaunchSound(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0==SceneManager.GetSceneByName(_levelScene)) _audioSource.Play();
    }

    public void BossKilled()
    {
        Debug.Log("Boss was killed");
        if (_levelLoaded == _maxLevelUnlocked)
        {
            _maxLevelUnlocked++;
        }
        LoadLevel(_levelLoaded+1);
    }

    public void LoadLevel(int level)
    {
        if (_maxLevelUnlocked < level) return;
        _levelLoaded = level;
        SceneManager.LoadScene(_levelScene);
    }

}

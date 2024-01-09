using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelLoaded;
    private int _maxLevelUnlocked;

    public int GetLevelLoaded() => _levelLoaded;
    public int GetMaxLevelUnlocked() => _maxLevelUnlocked;
}

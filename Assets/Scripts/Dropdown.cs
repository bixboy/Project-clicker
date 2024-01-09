using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    private int _multiplier;
    public event Action onValueChanged;

    public int GetMultiplier() => _multiplier;

    private void Awake()
    {
        DropdownValueChanged(0);
    }

    public void DropdownValueChanged(int index)
    {
        _multiplier = index switch
        {
            0 => 1,
            1 => 10,
            2 => 100,
            _ => throw new ArgumentOutOfRangeException()
        };
        onValueChanged.Invoke();
    }
}

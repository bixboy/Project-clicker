using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSummoner : MonoBehaviour
{
    [SerializeField] private GameObject _gameManagerPrefab;
    private void Awake()
    {
        GameObject manager = GameObject.FindWithTag("GameManager");
        if (manager == null)
        {
            GameObject gm = Instantiate(_gameManagerPrefab);
            DontDestroyOnLoad(gm);
        }
        Destroy(gameObject);
    }
}

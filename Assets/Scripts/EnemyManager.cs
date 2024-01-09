using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyAttack> _enemyList = new List<EnemyAttack>();
    private GameObject _boss;
    private LevelManager _levelManager;
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Boss"))
            {
                _boss = child.gameObject;
                continue;
            }
            _enemyList.Add(child.gameObject.GetComponent<EnemyAttack>());
        }

        _levelManager = GameObject.FindWithTag("GameManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        int level = _levelManager.GetLevelLoaded();
        
        //TODO: Set true value for appropriate scaling
        int damage = 10 * level;
        int maxHealth = 100 * level;
        float cooldownTime = (float) (1 - level * 0.01);
        foreach (EnemyAttack enemy in _enemyList)
        {
            enemy.SetStat(damage,maxHealth,cooldownTime);
        }
    }
}

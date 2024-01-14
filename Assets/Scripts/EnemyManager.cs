using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyAttack> _enemyList = new List<EnemyAttack>();
    private GameObject _boss;
    private LevelManager _levelManager;
    private UpgradeManager _upgradeManager; 
    [SerializeField] private UI _ui;
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
        _upgradeManager = _levelManager.gameObject.GetComponent<UpgradeManager>();
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
            EnemyLife enemyLife = enemy.GetEnemyLife();
            enemyLife.SetManager(_upgradeManager);
            enemyLife.OnDeath += _ui.AddCurrentEnemyCount;
        }

        _boss.GetComponent<EnemyAttack>().SetStat(damage * 2, maxHealth * 10, 4);
        EnemyLife bossLife = _boss.GetComponent<EnemyLife>();
        bossLife.SetManager(_upgradeManager);
        bossLife.OnDeath += _levelManager.BossKilled;
        //_boss.GetComponent<EnemyLife>().OnDeath += _levelManager.BossKilled;
        _ui.SetEnemyCount(_enemyList.Count);
    }
}

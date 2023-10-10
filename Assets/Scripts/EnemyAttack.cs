using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    private List<SoldiersLife> _soldats;

    [SerializeField] private float _cooldownTime;
    private float _lastAttackTime;


    // Update is called once per frame

    private void Awake()
    {
        _soldats= new List<SoldiersLife>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soldiers"))
        {
            _soldats.Add(collision.GetComponent<SoldiersLife>());
        }
    }

    private void Update()
    {
        if (_lastAttackTime + _cooldownTime < Time.time)
        {
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Soldiers") && _soldats.Contains(collision.GetComponent<SoldiersLife>()))
        {
            _soldats.Remove(collision.GetComponent<SoldiersLife>());
        }
    }

    private void Attack()
    {
        if (_soldats.Count == 0) { return; }
        List<SoldiersLife> tempSoldats = _soldats;
        int i = 0;
        int j = 0;
        while (i < _soldats.Count && j < 7)
        {
            if (!_soldats[i].TakeDamage(_damage)) { i++; } ;
            j++;
        }
        _lastAttackTime = Time.time;
    }
}

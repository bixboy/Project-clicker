using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int _damage;
    GameObject _soldats;

    [SerializeField] int _cooldown;
    bool canAttack = true;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soldiers"))
        {
            StartCoroutine(cooldown());
            if(canAttack)
            {
                _soldats = collision.gameObject.GetComponent<GameObject>();
                attack(_damage);
                canAttack = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Soldiers"))
        {
            StopCoroutine(cooldown());
        }
    }

    void attack(int damages)
    {
        _soldats.GetComponent<SoldiersLife>().TakeDamage(_damage);
    }

    private IEnumerator cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        canAttack = true;
    }
}

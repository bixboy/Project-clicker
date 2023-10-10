using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSoldats : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    [SerializeField] float _distStop;

    [SerializeField] LayerMask _layerMask; 

    [SerializeField] int _cooldown;
    [SerializeField] int _damage;
    bool _canAttack = false;

    private bool _destActif = false;
    bool _stopMove = false;
    Transform _destPoint;

    public void SetDestinationActif(bool isActif) => _destActif = isActif;

    private void Update()
    {
        if (_destActif)
        {
            float _distance = Vector2.Distance(transform.position, _destPoint.position);
            if (_distance > _distStop)
            {
                _stopMove = false;
                transform.position = Vector2.MoveTowards(transform.position, _destPoint.position, _moveSpeed * Time.deltaTime);
            }
            else
            {
                _stopMove = true;
            }
        }
        else
        {
            findDestPoint();
        }

        if (_stopMove && !_canAttack)
        {
            StartCoroutine(cooldown());
            _canAttack = true;
            Debug.Log("oui1");
        }
    }

    void findDestPoint()
    {
        RaycastHit2D rayHit;

        if (rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, _layerMask))
        {
            _destPoint = rayHit.transform;
            Debug.Log(_destPoint);
            _destActif = true;
        }
    }

    private IEnumerator cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        attack();
    }

    void attack()
    {
        if (_canAttack && _destPoint!=null)
        {
            _destPoint.GetComponent<EnemyLife>().TakeDamage(_damage);
            _canAttack = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSoldats : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    [SerializeField] float _distStop;

    [SerializeField] LayerMask _layerMask;

    public bool _destActif = false;
    Transform _destPoint;

    private void Update()
    {
        if (_destActif)
        {
            float _distance = Vector2.Distance(transform.position, _destPoint.position);
            if (_distance > _distStop)
            {
                transform.position = Vector2.MoveTowards(transform.position, _destPoint.position, _moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            findDestPoint();
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
}

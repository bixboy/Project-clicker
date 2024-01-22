using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Necromancer : MonoBehaviour, ISkill
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    private Animator _animator;
    private Rigidbody2D _rb;
    private int _action;
    private int _previousAction;
    private int _actionLength;
    [SerializeField] private float _lifeTime;
    private float _bornTime;
    [SerializeField] private float _speed;
    private bool _isActive;

    public void SetCam(CinemachineVirtualCamera camera) => _camera = camera;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _bornTime = Time.time;
        _isActive = true;

    }

    private void Update()
    {
        if (_bornTime + _lifeTime < Time.time)
        {
            _animator.SetTrigger("Death");
            if (_rb.velocity.x > 0)
            {
                _rb.velocity = Vector2.zero;
                _animator.SetFloat("Velocity", 0);
            }
            return;
        }
        float widthCam = Camera.main.aspect * _camera.m_Lens.OrthographicSize - 2;
        if (_camera.transform.position.x - widthCam >= transform.position.x)
        {
            _rb.velocity = new Vector2(_speed * Time.deltaTime, 0);
        }
        else _rb.velocity = new Vector2(_rb.velocity.x * 0.9f, 0);
        _animator.SetFloat("Velocity", _rb.velocity.x);
    }
    
    public void ActionFinished()
    {
        if (_actionLength>=3 && Random.Range(0, 2) == 1 || _actionLength>=10)
        {
            _actionLength = 0;
            while (_action==_previousAction)
            {
                _action = Random.Range(1, 4);
            }
            _animator.SetInteger("Action", _action);

            _previousAction = _action;
            return;
        }

        _actionLength++;

    }

    public void Kill()
    {
        Destroy(gameObject);
        _isActive = false;
    }
    

    private void SetActionToZero() => _animator.SetInteger("Action", 0);
    public void SetStat(int skillLevel)
    {
        _lifeTime = 20 + (10 * skillLevel);
    }

    public bool IsActive() => _isActive;

    public SkillName GetSkillName() => SkillName.Necromancier;
}

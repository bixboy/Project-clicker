using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatMovement : MonoBehaviour, ISkill
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    private Animator _animator;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;
    private CatAction _action;
    private CatAction _previousAction;
    private int _actionLength;
    private int _dir;
    private float _actionStart;
    [SerializeField] private float _lifeTime;
    private float _bornTime;

    private bool facingRight = true;
    private float[] _bordersX;
    private float _widthCam;
    private bool _isActive;

    public void SetCamera(CinemachineVirtualCamera cam)
    {
        _camera = cam;
        _widthCam = Camera.main.aspect * _camera.m_Lens.OrthographicSize;
    } 

    private void Start()
    {
        _bornTime = Time.time;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _previousAction = CatAction.Run;
        facingRight = true;
        _isActive = true;
    }

    private void Update()
    {
        _bordersX = new float[] {_camera.transform.position.x - _widthCam, _camera.transform.position.x + _widthCam};
        if (_bornTime + _lifeTime <= Time.time)
        {
            _isActive = false;
            if (facingRight){Flip();}
            _rb.velocity = new Vector2(-_runSpeed * Time.deltaTime, 0); 
            _animator.SetFloat("VelocityX", 10);
            if (transform.position.x<= _bordersX[0] - 1) Destroy(gameObject);
            return;
        }

        if (_bordersX[0] >= transform.position.x)
        {
            _rb.velocity = new Vector2(_runSpeed * Time.deltaTime, 0);
            _action = CatAction.Run;
        }
        else if (_bordersX[1] <= transform.position.x)
        {
            _rb.velocity = new Vector2(-_runSpeed * Time.deltaTime, 0);
            _action = CatAction.Run;
        }
        else
        {
            switch (_action)
            {
                case CatAction.Run:
                    _rb.velocity = new Vector2(_rb.velocity.x*0.9f,0);
                    break;
                case CatAction.Walking:
                    if (_actionStart+_actionLength > Time.time) _rb.velocity = new Vector2(_speed * _dir * Time.deltaTime, 0);
                    break;
                case CatAction.Sleeping:
                    if (_actionStart+_actionLength <= Time.time) _animator.SetBool("Sleep",false);
                    break;
            }
        }
        if (_rb.velocity.x>0.1)
        {
            if (!facingRight){Flip();}
        }
        else if (_rb.velocity.x<-0.1)
        {
            if (facingRight){Flip();}
        }
        _animator.SetFloat("VelocityX", Mathf.Abs(_rb.velocity.x));
    }
    
    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
        facingRight = !facingRight;
    }

    public void ActionFinished()
    {
        if (Random.Range(0, 2) == 1 || _actionLength>=5)
        {
            _actionLength = 0;
            while (_action==_previousAction)
            {
                _action = (CatAction) Random.Range(1, 5);
            }

            switch (_action)
            {
                case CatAction.Meow:
                    _animator.SetTrigger("Meow");
                    break;
                case CatAction.Sleeping:
                    _animator.SetBool("Sleep", true);
                    _actionStart = Time.time;
                    _actionLength = Random.Range(2, 6);
                    break;
                case CatAction.Stretching:
                    _animator.SetTrigger("Stretch");
                    break;
                case CatAction.Walking:
                    _dir = Random.Range(0, 2) == 0 ? -1 : 1;
                    if (transform.position.x - 1 < _bordersX[0]) _dir = 1;
                    else if (transform.position.x + 1 > _bordersX[1]) _dir = -1;
                    _actionStart = Time.time;
                    _actionLength = Random.Range(1, 6);
                    break;
            }
            _previousAction = _action;
            return;
        }

        _actionLength++;

    }
    private void OnDrawGizmos()
    {
        float widthCam = Camera.main.aspect * _camera.m_Lens.OrthographicSize;
        float[] bordersX = {_camera.transform.position.x - widthCam, _camera.transform.position.x + widthCam};
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(bordersX[0],_camera.transform.position.y,0),new Vector3(1,1,0));
        Gizmos.DrawWireCube(new Vector3(bordersX[1],_camera.transform.position.y,0),new Vector3(1,1,0));

    }
    
    public enum CatAction
    {
        Run=0,
        Sleeping=1,
        Walking=2,
        Stretching=3,
        Meow=4,
    }

    public void SetStat(int skillLevel)
    {
        _lifeTime = 20 + (10 * skillLevel);
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public SkillName GetSkillName()
    {
        return SkillName.Cats;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class NinjaMove : MonoBehaviour, ISkill
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distStop;
    [SerializeField] private float _distStopBoss;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _damage;
    [SerializeField] private float _baseCooldown;
    [SerializeField] private int _damageExplosion;
    [SerializeField] private float _radiusExplosion;
    private float _cooldown;

    private UpgradeManager _upgradeManager;
    private SoldiersLife _ninjaLife;

    private Transform _destPoint;
    private Animator _animator;

    private bool _destActive = false;
    [SerializeField] private bool _secondAttack = false;
    private bool _canAttack = false;
    private bool _stopMove = false;
    private bool _firstAttack = true;

    private List<EnemyLife> _enemy;

    public void SetDestinationActif(bool isActive) => _destActive = isActive;

    void Start()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _damage = (int)_upgradeManager.GetUpgradeStatByName(StatName.AttackDamage).Amount;
        _cooldown = _baseCooldown / _upgradeManager.GetUpgradeStatByName(StatName.AttackSpeed).Amount;
        _animator = GetComponent<Animator>();
        _ninjaLife = GetComponent<SoldiersLife>();
        GetComponent<CircleCollider2D>().radius = _radiusExplosion;
        _enemy = new List<EnemyLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_ninjaLife._isDead)
        {
            if (_destActive && _destPoint != null)
            {
                float distance = Vector2.Distance(transform.position, _destPoint.position);
                if (distance > _distStop)
                {
                    _stopMove = false;
                    _animator.SetBool("Move", true);
                    transform.position = Vector2.MoveTowards(transform.position, _destPoint.position, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    _animator.SetBool("Move", false);
                    _stopMove = true;
                }
            }
            else
            {
                if (FindDestPoint() == false)
                {
                    _destPoint = null;
                    _destActive = false;
                    _animator.SetBool("Move", false);
                }
            }

            if (_stopMove && !_canAttack && this.CompareTag("Soldiers"))
            {
                StartCoroutine(Cooldown());
                _canAttack = true;
            }
        }
    }

    private bool FindDestPoint()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, _layerMask);
        if (!rayHit) return false;
        if (rayHit.collider.CompareTag("Boss")) _distStop = _distStopBoss;
        _destPoint = rayHit.transform;
        Debug.Log(_destPoint);
        _destActive = true;
        return true;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        Attack();
    }

    private void Attack()
    {
        if (_canAttack && _destPoint != null && this.CompareTag("Soldiers") && _ninjaLife.GetCurrentHealth() > 0)
        {
            if (_firstAttack)
            {
                _animator.SetTrigger("Attack");
                _firstAttack = false;
            }
            else if (_secondAttack)
            {
                _animator.SetTrigger("SecondAttack");
                _firstAttack = true;
            }
            else 
            {
                _animator.SetTrigger("Attack");
                _firstAttack = false;
            }
            bool crit = Random.Range(0, 100) < (int)_upgradeManager.GetUpgradeStatByName(StatName.CritChance).Amount;
            _destPoint.GetComponent<EnemyLife>().TakeDamage(crit ? _damage : _damage * 2);
            _canAttack = false;
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemy.Add(collision.GetComponent<EnemyLife>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _enemy.Contains(collision.GetComponent<EnemyLife>()))
        {
            _enemy.Remove(collision.GetComponent<EnemyLife>());
        }
    }

    private void Explosion()
    {
        if (_ninjaLife.GetCurrentHealth() <= 0) 
        {
            bool crit = Random.Range(0, 100) < (int)_upgradeManager.GetUpgradeStatByName(StatName.CritChance).Amount;
            MultipleAttack(crit);
        }
    }

    private void MultipleAttack(bool crit)
    {
        if (_enemy.Count == 0) { return; }
        List<EnemyLife> tempEnemy = _enemy;
        int i = 0;
        int j = 0;
        while (i < _enemy.Count && j < 7)
        {
            _animator.SetTrigger("Attack");
            _enemy[i].TakeDamage(crit ? _damageExplosion : _damageExplosion * 2);
            i++;
            j++;
        }
    }

    public void SetStat(int skillLevel)
    {
        if (_ninjaLife==null) _ninjaLife = GetComponent<SoldiersLife>();
        _ninjaLife.NinjaLife(skillLevel);
    }

    public bool IsActive()
    {
        return false;
    }

    public SkillName GetSkillName()
    {
        return SkillName.NinjaSoldier;
    }
}

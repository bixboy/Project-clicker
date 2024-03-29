using System.Collections;
using spawn;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveSoldats : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distStop;
    [SerializeField] private float _distStopBoss;

    [SerializeField] private LayerMask _layerMask; 

    [SerializeField] private float _baseCooldown;
    private float _cooldown;
    [SerializeField] private int _damage;
    private bool _canAttack = false;
    private UpgradeManager _upgradeManager;
    private InvokeSoldats _invoker;

    public void SetInvoker(InvokeSoldats invoker) => _invoker = invoker;

    private SoldiersLife _soldiersLife;
    private int life;

    [SerializeField]
    private bool _destActive = false;
    private bool _stopMove = false;
    private Transform _destPoint;
    private Animator _animator;

    private bool _firstAttack = true;

    public void SetDestinationActif(bool isActive) => _destActive = isActive;

    private void Awake()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _damage = (int)_upgradeManager.GetUpgradeStatByName(StatName.AttackDamage).Amount;
        _cooldown = _baseCooldown / _upgradeManager.GetUpgradeStatByName(StatName.AttackSpeed).Amount;
        _animator = GetComponent<Animator>();
        _soldiersLife = GetComponent<SoldiersLife>();
        _animator.SetFloat("AttackSpeed",1+ _upgradeManager.GetUpgradeStatByName(StatName.AttackSpeed).Amount/10);
    }

    private void Update()
    {   
        if(!GetComponent<SoldiersLife>()._isDead)
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
        yield return new WaitForSeconds(_invoker.isSkillActive(SkillName.Cats)? _cooldown/3 : _cooldown);
        Attack();
    }

    private void Attack()
    {
        life = _soldiersLife.GetCurrentHealth();
        if (_canAttack && _destPoint!=null && this.CompareTag("Soldiers") && life > 0)
        {
            if (_firstAttack) 
            {
                _animator.SetTrigger("Attack");
                _firstAttack = false;
            }
            else
            {
                _animator.SetTrigger("SecondAttack");
                _firstAttack = true;
            }
            bool crit = Random.Range(0, 100) < (int)_upgradeManager.GetUpgradeStatByName(StatName.CritChance).Amount;
            _destPoint.GetComponent<EnemyLife>().TakeDamage(crit? _damage : _damage*2);
            _canAttack = false;
        }
    }
}

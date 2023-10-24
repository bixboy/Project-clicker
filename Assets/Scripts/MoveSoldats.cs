using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveSoldats : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distStop;

    [SerializeField] private LayerMask _layerMask; 

    [SerializeField] private float _baseCooldown;
    private float _cooldown;
    [SerializeField] private int _damage;
    private bool _canAttack = false;
    private UpgradeManager _upgradeManager;

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
        _damage = (int)_upgradeManager.GetUpgradeByName(StatName.AttackDamage).Amount;
        _cooldown = _baseCooldown / _upgradeManager.GetUpgradeByName(StatName.AttackSpeed).Amount;
        _animator = GetComponent<Animator>();
    }

    private void Update()
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
            FindDestPoint();
        }

        if (_stopMove && !_canAttack)
        {
            StartCoroutine(Cooldown());
            _canAttack = true;
            Debug.Log("oui1");
        }
    }

    private void FindDestPoint()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, _layerMask);
        if (!rayHit) return;
        _destPoint = rayHit.transform;
        Debug.Log(_destPoint);
        _destActive = true;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        Attack();
    }

    private void Attack()
    {
        if (_canAttack && _destPoint!=null)
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
            bool crit = Random.Range(0, 100) < (int)_upgradeManager.GetUpgradeByName(StatName.CritChance).Amount;
            _destPoint.GetComponent<EnemyLife>().TakeDamage(crit? _damage : _damage*2);
            _canAttack = false;
        }
    }
}

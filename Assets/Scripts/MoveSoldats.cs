using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveSoldats : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distStop;

    [SerializeField] private LayerMask _layerMask; 

    [SerializeField] private float _cooldown;
    [SerializeField] private int _damage;
    private bool _canAttack = false;
    private UpgradeManager _upgradeManager;

    private bool _destActive = false;
    private bool _stopMove = false;
    private Transform _destPoint;

    public void SetDestinationActif(bool isActive) => _destActive = isActive;

    private void Awake()
    {
        _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
        _damage = (int)_upgradeManager.GetUpgradeByName(StatName.AttackDamage).Amount;
        _cooldown = _cooldown / _upgradeManager.GetUpgradeByName(StatName.AttackSpeed).Amount;
    }

    private void Update()
    {
        if (_destActive)
        {
            float distance = Vector2.Distance(transform.position, _destPoint.position);
            if (distance > _distStop)
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
            bool crit = Random.Range(0, 100) < (int)_upgradeManager.GetUpgradeByName(StatName.CritChance).Amount;
            _destPoint.GetComponent<EnemyLife>().TakeDamage(crit? _damage : _damage*2);
            _canAttack = false;
        }
    }
}

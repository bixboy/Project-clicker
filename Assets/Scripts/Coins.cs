using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int _valueGold;
    private UpgradeManager _upgradeManager;

    private Rigidbody2D _rigidbodyCoin;
    [SerializeField] private float throwForce;
    [SerializeField]  private float bounceForce;
    [SerializeField]  private float dampingFactor;

    private int _bounceCount;

    [SerializeField] private float _timeClearCoins;

    public void SetStats(UpgradeManager upgradeManager, int value)
    {
        _upgradeManager = upgradeManager;
        _valueGold = value;
    }

    private void Start()
    {
        throwForce = Random.Range(200, 350);
        _rigidbodyCoin = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.Range(1.4f,-1), Random.Range(0.5f, 1f)).normalized;
        _rigidbodyCoin.AddForce(direction * throwForce);
        Destroy(gameObject, _timeClearCoins);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _bounceCount < 2)
        {
            _rigidbodyCoin.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

            _rigidbodyCoin.velocity *= dampingFactor;

            _bounceCount++;
        }

        if (collision.gameObject.CompareTag("Soldiers")) { _upgradeManager.CollectCoin(_valueGold); Destroy(gameObject); }
    }
    private void OnDestroy()
    {
        _upgradeManager.CollectCoin(_valueGold);
    }
}

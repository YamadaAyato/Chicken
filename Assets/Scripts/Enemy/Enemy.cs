using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("HP�ݒ�")]
    [SerializeField] private int _maxHp = 3;�@//HP�ݒ�
    private int _currentHp;

    [Header("���x�ݒ�")]
    [SerializeField] private float _moveForce = 10f;�@//�ړ����x
    [SerializeField] private float _maxSpeed = 50f;�@//���x����
    private float _baseMoveForce;

    [Header("�ݑ��ݒ�")]
    [SerializeField] private float _slowMultiplier = 0.5f;  // ���x�����{�ɂ��邩
    [SerializeField] private float _slowDuration = 1f;      // �ݑ����ԁi�b�j
    private bool _isSlowed = false;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHp = _maxHp;
        _baseMoveForce = _moveForce;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        if (_rb.velocity.x > -_maxSpeed)
        {
            _rb.AddForce(Vector2.left * _moveForce);
        }

        //�ő呬�x�𐧌�
        if (_rb.velocity.x < -_maxSpeed)
        {
            _rb.velocity = new Vector2(-_maxSpeed, _rb.velocity.y);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ApplySlow()
    {
        if (_isSlowed) return;
        StartCoroutine(Slow());
    }

    private IEnumerator Slow()
    {
        _isSlowed = true;
        _moveForce = _baseMoveForce * _slowMultiplier;

        yield return new WaitForSeconds(_slowDuration);

        _moveForce = _baseMoveForce;
        _isSlowed = false;
    }
}



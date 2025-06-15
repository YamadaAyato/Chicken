using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("HP�ݒ�")]
    [SerializeField] private int _maxHp = 3;�@//HP�ݒ�
    private int _currentHp;

    [Header("���x�ݒ�")]
    [SerializeField] private float _baseSpeed = 10f;�@//�ړ����x
    [SerializeField] private float _maxSpeed = 50f;�@//���x����
    private float _currentSpeed;

    [Header("�W�����v�ݒ�")]
    [SerializeField] private float _jumpForce = 10f;             // �W�����v��
    [SerializeField] private float _jumpHorizontalSpeed = 5f;    // �������ւ̃W�����v���x

    [Header("�ݑ��ݒ�")]
    [SerializeField] private float _slowMultiplier = 0.5f;  // ���x�����{�ɂ��邩
    [SerializeField] private float _slowDuration = 1f;      // �ݑ����ԁi�b�j

    [Header("�G�ʐݒ�")]
    [SerializeField] private bool _move;�@//���ړ�
    [SerializeField] private bool _jump;�@//�W�����v�ړ�

    private bool _isSlowed = false;�@�@//�X���[��Ԃ̔���
    private bool _isGrounded = false;�@//�ڒn����

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHp = _maxHp;
        _currentSpeed = _baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_move)
        {
            EnemyMove();
        }

        if (_jump && _isGrounded) 
        {
            EnemyJump();
        }
    }

    private void EnemyMove()
    {
        // �������Ɉ�葬�x�ňړ��i���x��������j
        //Mathf.Clamp(�����������l, �ŏ��l, �ő�l)
        float clampedSpeed = Mathf.Clamp(-_currentSpeed, -_maxSpeed, _maxSpeed);
        //_rb.velocity.y �� y�����̑��x�i�W�����v�E�����Ȃǁj��ێ�
        _rb.velocity = new Vector2(clampedSpeed, _rb.velocity.y);
    }

    private void EnemyJump()
    {
        // ���΂ߏ�ɃW�����v
        _rb.velocity = new Vector2(-_jumpHorizontalSpeed, _jumpForce);
        _isGrounded = false; // �󒆂ɂ����Ԃɖ߂�
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
        //_isSlowed�����s����Ă���Ȃ���s���Ȃ�
        if (_isSlowed) return;
        StartCoroutine(Slow());
    }

    private IEnumerator Slow()
    {
        _isSlowed = true;
        //�ړ����x�ɒx������{����������
        _currentSpeed = _baseSpeed * _slowMultiplier;
        //���Ԍo�ߌ�A���̏��������s
        yield return new WaitForSeconds(_slowDuration);
        //�ړ����x��߂�
        _currentSpeed = _baseSpeed; ;
        _isSlowed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}



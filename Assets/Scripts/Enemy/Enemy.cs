using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int _maxHp = 3;　//HP設定
    private int _currentHp;

    [Header("速度設定")]
    [SerializeField] private float _baseSpeed = 10f;　//移動速度
    [SerializeField] private float _maxSpeed = 50f;　//速度制限
    private float _currentSpeed;

    [Header("ジャンプ設定")]
    [SerializeField] private float _jumpForce = 10f;             // ジャンプ力
    [SerializeField] private float _jumpHorizontalSpeed = 5f;    // 左方向へのジャンプ速度

    [Header("鈍足設定")]
    [SerializeField] private float _slowMultiplier = 0.5f;  // 速度を何倍にするか
    [SerializeField] private float _slowDuration = 1f;      // 鈍足時間（秒）

    [Header("敵別設定")]
    [SerializeField] private bool _move;　//横移動
    [SerializeField] private bool _jump;　//ジャンプ移動

    [Header("スコア設定")]
    [SerializeField] public int _scoreValue = 5; //敵を倒した時に加算するスコア

    private bool _isSlowed = false;　　//スロー状態の判定
    private bool _isGrounded = false;　//接地判定

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
        // 左方向に一定速度で移動（速度制限あり）
        //Mathf.Clamp(制限したい値, 最小値, 最大値)
        float clampedSpeed = Mathf.Clamp(-_currentSpeed, -_maxSpeed, _maxSpeed);
        //_rb.velocity.y で y方向の速度（ジャンプ・落下など）を保持
        _rb.velocity = new Vector2(clampedSpeed, _rb.velocity.y);
    }

    private void EnemyJump()
    {
        // 左斜め上にジャンプ
        _rb.velocity = new Vector2(-_jumpHorizontalSpeed, _jumpForce);
        _isGrounded = false; // 空中にいる状態に戻す
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
        ScoreManager.Instance.AddScore(_scoreValue);
        Destroy(gameObject);
    }

    public void ApplySlow()
    {
        //_isSlowedが実行されているなら実行しない
        if (_isSlowed) return;
        StartCoroutine(Slow());
    }

    private IEnumerator Slow()
    {
        _isSlowed = true;
        //移動速度に遅くする倍率をかける
        _currentSpeed = _baseSpeed * _slowMultiplier;
        //時間経過後、下の処理を実行
        yield return new WaitForSeconds(_slowDuration);
        //移動速度を戻す
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



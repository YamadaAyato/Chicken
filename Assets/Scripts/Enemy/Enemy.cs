using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action OnAnyEnemyDied;

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
    [SerializeField] private int _attackHomeDamage = 1;  //家への攻撃力
    [SerializeField] private int _knockBackForce = 5;

    [Header("スコア設定")]
    [SerializeField] public int _scoreValue = 5; //敵を倒した時に加算するスコア

    [Header("効果音設定")]
    [SerializeField] AudioClip _seClip;
    private AudioSource _audioSource;

    private bool _isSlowed = false;　　//スロー状態の判定
    private bool _isGrounded = false;　//接地判定

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        _currentHp = _maxHp;  //現在のHpにmaxHpを代入
        _currentSpeed = _baseSpeed;  //現在のSpeedにmaxSpeedを代入

        Vector3 localScale = transform.localScale;
        localScale.x = -Mathf.Abs(localScale.x); // 左向き
        transform.localScale = localScale;
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

    /// <summary>
    /// 敵の水平移動
    /// </summary>
    private void EnemyMove()
    {
        // 左方向に一定速度で移動（速度制限あり）
        //Mathf.Clamp(制限したい値, 最小値, 最大値)
        float clampedSpeed = Mathf.Clamp(-_currentSpeed, -_maxSpeed, _maxSpeed);
        //_rb.velocity.y で y方向の速度（ジャンプ・落下など）を保持
        _rb.velocity = new Vector2(clampedSpeed, _rb.velocity.y);
    }

    /// <summary>
    /// 敵のジャンプ移動
    /// </summary>
    private void EnemyJump()
    {
        // 左斜め上にジャンプ
        _rb.velocity = new Vector2(-_jumpHorizontalSpeed, _jumpForce);
        _isGrounded = false; // 空中にいる状態に戻す
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">EggControlerから値を代入</param>
    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        if (_audioSource != null && _seClip != null)
        {
            _audioSource.clip = _seClip;
            _audioSource.Play(); 
        }

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// HPが0になった時に呼び出される処理
    /// </summary>
    private void Die()
    {
        OnAnyEnemyDied?.Invoke();
        ScoreManager.Instance.AddScore(_scoreValue);
        Destroy(gameObject);
    }

    /// <summary>
    /// FriedEggSlowZoneから呼び出されSlowメソッドを呼び出す
    /// </summary>
    public void ApplySlow()
    {
        //_isSlowedが実行されているなら実行しない
        if (_isSlowed) return;
        StartCoroutine(Slow());
    }

    /// <summary>
    /// 敵を遅くする処理
    /// </summary>
    private IEnumerator Slow()
    {
        _isSlowed = true;
        //移動速度に遅くする倍率をかける
        _currentSpeed = _baseSpeed * _slowMultiplier;
        //時間経過後、下の処理を実行
        yield return new WaitForSeconds(_slowDuration);
        //移動速度を戻す
        _currentSpeed = _baseSpeed;
        _isSlowed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Home"))
        {
            _move = false;
            Home _home = collision.gameObject.GetComponent<Home>();
            //Home.csから呼び出す
            _home.TakeDamegeHome(_attackHomeDamage);  
            Debug.Log($"家へ{_attackHomeDamage}ダメージ");  

            //家に当たったら力を加えノックバックする
            _rb.AddForce(Vector2.right * _knockBackForce, ForceMode2D.Impulse);

            Invoke("ChangeMoveFlag", 3f);
        }
    }

    /// <summary>
    /// 3秒後に_moveをtrueに
    /// </summary>
    private void ChangeMoveFlag()
    {
        if (!_jump)
        {
            _move = true;
        }
    }
}



﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _egg;

    [Header("移動設定")]
    [SerializeField] private float _moveSpeed = 10f;　//移動速度
    [SerializeField] private float _jumpforce = 10f;　//ジャンプ力
    [SerializeField] private float _maxSpeed = 50f; //速度上限
    bool _IsGrounded = false;

    [Header("卵設定")]
    [SerializeField] private float _eggDropForce = 10f; //卵を落とす力
    [SerializeField] private float _fireInterval = 2f;　//卵を落とす間隔

    [Header("必殺技設定")]
    [SerializeField] private float _maxSpecialGauge = 100f;
    [SerializeField] private float _gaugePerEnemy = 20f;
    [SerializeField] private float _specialRange = 5f; // 範囲
    [SerializeField] private LayerMask _enemyLayer;
    private float _currentSpecialGauge = 0f;

    [Header("効果音関連")]
    [SerializeField] private AudioClip _stunClip;
    [SerializeField] private AudioClip _specialClip;
    private AudioSource _audioSource;

    [Header("アニメーション関連")]
    [SerializeField] private GameObject _cutIn;
    [SerializeField] private Animator _specialEffectAnimator; //必殺技演出用のAnimator

    private bool _isStunned;  //スタンしているか
    private float _stunTimer;  //スタン用の時間
    private Rigidbody2D _rb;
    float _timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        _timer += Time.deltaTime;

        if (_isStunned)
        {
            _stunTimer -= Time.deltaTime;
            if (_stunTimer <= 0)
            {
                _isStunned = false;
                Debug.Log("スタン解除");
            }

            return; // スタン中は操作不可
        }

        if (Input.GetButtonDown("Fire1"))
        {
            DropEgg();
        }

        //IsGroundedがtrueの時とshiftが押されたときジャンプメソッドを実行
        if (_IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire2") && _currentSpecialGauge >= _maxSpecialGauge)
        {
            UseSpecialSkill();
        }
    }

    private void FixedUpdate()
    {
        if (_isStunned)
        {
            _stunTimer -= Time.deltaTime;
            if (_stunTimer <= 0)
            {
                _isStunned = false;
                Debug.Log("スタン解除");
            }

            return; // スタン中は操作不可
        }

        Move();
    }

    /// <summary>
    /// プレイヤーの水平移動の処理
    /// </summary>
    private void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (_rb != null)
        {
            //速度が_maxSpeedでなければ力を加える
            if (Mathf.Abs(_rb.velocity.x) < _maxSpeed)
            {
                _rb.AddForce(Vector2.right * move * _moveSpeed);
            }
        }
    }

    /// <summary>
    /// プレイヤーのジャンプの処理
    /// </summary>
    private void Jump()
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            //設置判定の変数をfalseに
            _IsGrounded = false;
        }
    }

    /// <summary>
    /// 左クリックしたときに卵を落とす処理
    /// </summary>
    private void DropEgg()
    {
        if (_timer > _fireInterval)
        {
            //マズルの位置から卵を生成
            GameObject egg = Instantiate(_egg, _muzzle.transform.position, Quaternion.identity);

            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            //卵に落ちる力を加える
            eggRb.AddForce(Vector2.down * _eggDropForce, ForceMode2D.Impulse);
            //時間計測をリセット
            _timer = 0f;
        }
    }

    /// <summary>
    /// StunBirdで呼び出し、プレイヤーをスタンさせる
    /// </summary>
    /// <param name="duration"></param>
    public void Stun(float duration)
    {
        _isStunned = true;
        _stunTimer = duration;
        Debug.Log($"スタン中{duration}秒");
        PlayStunSE(duration);
    }

    public void PlayStunSE(float duration)
    {
        _audioSource.clip = _stunClip;
        _audioSource.Play();
        Invoke(nameof(StopSE), duration);
    }

    private void StopSE()
    {
        _audioSource.Stop();
    }

    /// <summary>
    /// 必殺技を使うときの処理
    /// </summary>
    private void UseSpecialSkill()
    {
        Debug.Log("ULT発動");
        _audioSource.PlayOneShot(_specialClip);

        _cutIn.SetActive(true);

        if (_specialEffectAnimator != null)
        {
            _specialEffectAnimator.SetTrigger("Special"); // 必殺技Animatorのトリガーを引く
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _specialRange, _enemyLayer);
        foreach (Collider2D _enemy in enemies)
        {
            Enemy enemy = _enemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(3); // 大ダメージ
            }
        }

        _currentSpecialGauge = 0f;
    }

    /// <summary>
    /// 必殺技のゲージをためる処理
    /// </summary>
    public void AddSpecialGauge()
    {
        _currentSpecialGauge += _gaugePerEnemy;
        _currentSpecialGauge = Mathf.Min(_currentSpecialGauge, _maxSpecialGauge);
        Debug.Log($"ゲージ増加: {_currentSpecialGauge}/{_maxSpecialGauge}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //足場と接触しているとき、接地判定の変数をtrueに
        if (collision.gameObject.CompareTag("Scaffold"))
        {
            Debug.Log("地面にあたっています");
            _IsGrounded = true;
        }
    }

    //安全用
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Scaffold"))
    //    {
    //        _IsGrounded = true;
    //    }
    //}

    private void OnEnable()
    {
        Enemy.OnAnyEnemyDied += AddSpecialGauge;
    }

    private void OnDisable()
    {
        Enemy.OnAnyEnemyDied -= AddSpecialGauge;
    }

    public float CurrentGaugeRatio
    {
        get { return _currentSpecialGauge / _maxSpecialGauge; }
    }
}

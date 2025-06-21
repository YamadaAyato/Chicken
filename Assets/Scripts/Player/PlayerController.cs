using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _egg;

    [Header("移動設定")]
    [SerializeField] private float _moveSpeed = 10f;　//移動速度
    [SerializeField] private float _jumpforce = 10f;　//ジャンプ力
    [SerializeField] private float _maxSpeed = 50f;　//速度上限

    [Header("卵設定")]
    [SerializeField] private float _eggDropForce = 10f; //卵を落とす力
    [SerializeField] private float _fireInterval = 2f;　//卵を落とす間隔

    private bool _IsStunned;  //スタンしているか
    private float _stuntimer;  //スタン用の時間
    private Rigidbody2D _rb;
    bool IsGrounded = false;
    float _timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        _timer += Time.deltaTime;

        if (_IsStunned)
        {
            _stuntimer -= Time.deltaTime;
            if (_stuntimer <= 0)
            {
                _IsStunned = false;
                Debug.Log("スタン解除");
            }

            return; // スタン中は操作不可
        }
        Move();

        DropEgg();

        //IsGroundedがtrueの時とshiftが押されたときジャンプメソッドを実行
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
    }

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

    private void Jump()
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            //設置判定の変数をfalseに
            IsGrounded = false;
        }
    }

    private void DropEgg()
    {
        if (Input.GetButtonDown("Fire1") && _timer > _fireInterval)
        {
            //マズルの位置から卵を生成
            GameObject egg = Instantiate(_egg,_muzzle.transform.position, Quaternion.identity);

            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            //卵に落ちる力を加える
            eggRb.AddForce(Vector2.down * _eggDropForce, ForceMode2D.Impulse);
            //時間計測をリセット
            _timer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //足場と接触しているとき、接地判定の変数をtrueに
        if (collision.gameObject.CompareTag("Scaffold"))
        {
            Debug.Log("地面にあたっています");
            IsGrounded = true;
        }
    }
    public void Stun(float _duration)
    {
        _IsStunned = true;
        _stuntimer = _duration;
        Debug.Log($"スタン中{_duration}秒");
    }
}

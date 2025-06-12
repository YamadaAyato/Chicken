using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _egg;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpforce = 10f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _eggDropForce = 10f;
    [SerializeField] private float _fireInterval = 2f;
    private Rigidbody2D _rb;
    bool IsGrounded = false;
    float _timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D がアタッチされていません！");
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Move();
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        DropEgg();
    }
    private void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (_rb != null)
        {
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
            IsGrounded = false;
        }
    }

    private void DropEgg()
    {
        if (Input.GetButtonDown("Fire1") && _timer > _fireInterval)
        {
            GameObject egg = Instantiate(_egg,_muzzle.transform.position, Quaternion.identity);

            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            eggRb.AddForce(Vector2.down * _eggDropForce, ForceMode2D.Impulse);
            _timer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scaffold"))
        {
            Debug.Log("地面にあたっています");
            IsGrounded = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoler : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpforce = 10f;
    [SerializeField] private float _maxSpeed = 10f;
    private Rigidbody2D _rb;
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
        Move();
        Jump();
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
        if (Input.GetButtonDown("Jump") && _rb != null)
        {
            _rb.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
        }
    }

}

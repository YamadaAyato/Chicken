using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHp = 3;
    [SerializeField] private float _moveForce = 10f;
    [SerializeField] private float _maxSpeed = 50f;
    // Start is called before the first frame update
    private int _currentHp;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }
    
    private void EnemyMove()
    {
        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.AddForce(Vector2.left * _moveForce);
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
}

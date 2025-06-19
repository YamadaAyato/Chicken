using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] GameObject _friedEggPrefab;  //目玉焼きのプレハブ

    void Start()
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(_friedEggPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy =collision.GetComponent<Enemy>();
            Debug.Log(_enemy);
            _enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

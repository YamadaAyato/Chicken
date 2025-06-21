using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEggSlowZone : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject,_lifeTime);  //_lifeTimeたったら消す
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();  //相手のEnemyスクリプトをGetComponent
            enemy.ApplySlow();  //呼び出す
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBird : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;  //前進スピード
    [SerializeField] private float _waveSpeed;  //波の速さ
    [SerializeField] private float _waveHeight;　 //波の高さ
    [SerializeField] private float _stunDuration;  //プレイヤーのスタン時間
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var stunnable = collision.gameObject.GetComponent("Stunnable");

            Destroy(gameObject);
        }
    }
}

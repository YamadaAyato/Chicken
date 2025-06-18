using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StunBird : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float _moveSpeed = 5f;  //前進スピード
    [SerializeField] private float _waveSpeed = 2f;  //波の速さ
    [SerializeField] private float _waveHeight = 1f;  //波の高さ 
    public Vector3 direction = Vector3.right; // 移動方向（右に進む）

    [Header("スタン設定")]
    [SerializeField] private float _stunDuration;  //プレイヤーのスタン時間

    private Vector2 _startPosition;  //初期位置
    private float _waveOffset;  //波の開始位相


    // Start is called before the first frame update
    void Start()
    {
        //初期位置を記録
       _startPosition = transform.position;
        // サイン波のスタート位置をランダム化
        _waveOffset = Random.Range(0f, Mathf.PI * 2f);
        // 方向ベクトルを正規化（安全のため）
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //右への直進移動
        //フレームレートに依存しない動きを実現するために時間をかける
        Vector3 forwardMove = new Vector3(direction.x, direction.y, 0f) * _moveSpeed * Time.deltaTime;
        
        // サイン波で上下に揺れるようにY座標を加算
        float wave = Mathf.Sin(Time.time * _waveSpeed + _waveOffset) *_waveHeight;
        
        // 上下移動分をTime.deltaTimeで滑らかに補正
        Vector3 waveOffsetY = new Vector3(0f, wave, 0f) * Time.deltaTime;
       
        // 合成して移動
        transform.position += forwardMove + waveOffsetY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var stunnable = collision.gameObject.GetComponent<PlayerController>();
            stunnable.Stun(_stunDuration); // 指定秒数だけスタンさせる

            Destroy(gameObject);
        }
    }
}

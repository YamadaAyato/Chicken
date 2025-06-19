using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("スポーン設定")]
    [SerializeField] private StunBird _stunBirdPrefab;  //スポーンさせる敵のプレハブ
    [SerializeField] private Transform[] _spawnPoints;　　//出現位置

    [Header("スポーンの間隔")]
    [SerializeField] private float _minInterval = 1f;  //出現の最小間隔
    [SerializeField] private float _maxInterval = 5f;  //出現の最大間隔
    private float _spawntimer;

    void Start()
    {
        //_spawntimerにスポーン間隔の値からランダムに数を取り出し代入
        _spawntimer = Random.Range(_minInterval, _maxInterval);
    }

    void Update()
    {
        _spawntimer -= Time.deltaTime;

        //スポーンタイマーが0以下の時
        if (_spawntimer <= 0)
        {
            Spawn();
            _spawntimer = Random.Range(_minInterval, _maxInterval);
        }
    }

    void Spawn()
    {
        //配列の要素数からランダムに数を出し、その数のポイントを代入
        int index = Random.Range(0, _spawnPoints.Length);
        Transform _spawnPoint = _spawnPoints[index];

        //スポーンポイントに敵を召喚
        StunBird enemy = Instantiate(_stunBirdPrefab, _spawnPoint.position, Quaternion.identity);
    }
}

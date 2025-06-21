using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region PhaseClassDefinition
    // 各フェーズの設定クラス
    //[System.Serializable]は、ScriptableObject や MonoBehaviour の中で、複雑なデータ構造を扱いたいとき
    //リストや配列の要素として独自のクラスを表示したいとき。
    [System.Serializable]
    public class Phase
    {
        public GameObject[] EnemyPrefabs;    // このフェーズで出現する敵プレハブ
        public GameObject BossPrefab;        // このフェーズで出現させるボスのプレハブ（nullで出現なし）
        public bool SpawnBoss = false;       // このフェーズでボスを出現させるか
        public float SpawnInterval = 3f;     // 敵をスポーンする間隔
        public int MinSpawnCount = 1;        // 一度に出す敵の最小数
        public int MaxSpawnCount = 3;        // 一度に出す敵の最大数
    }
    #endregion

    public Phase[] Phases;                  // 各フェーズの設定リスト
    public float PhaseDuration = 15f;       // 各フェーズの長さ
    public Transform[] SpawnPoints;         // 敵の出現位置　

    private int _currentPhaseIndex = 0;      // フェーズ番号
    private float _phaseTimer = 0f;          // フェーズの経過時間
    private float _spawnTimer = 0f;          // 敵のスポーン用タイマー
    private bool _bossSpawned = false;       // このフェーズでボスが出現済みか

    void Update()
    {
        _phaseTimer += Time.deltaTime;  //フェーズの経過時間

        // フェーズの持続時間を超えたら次のフェーズへ
        if (_currentPhaseIndex < Phases.Length - 1
            && _phaseTimer >= PhaseDuration)
        {
            _currentPhaseIndex++;
            _phaseTimer = 0f;
            _spawnTimer = 0f;
            _bossSpawned = false;  // ボス出現フラグをリセット
            Debug.Log($"フェーズ {_currentPhaseIndex + 1} に移行");
        }

        var currentPhase = Phases[_currentPhaseIndex];

        // ボスの出現処理
        if (currentPhase.SpawnBoss 
            && !_bossSpawned 
            && currentPhase.BossPrefab != null)
        {
            SpawnBoss(currentPhase.BossPrefab);
            _bossSpawned = true;
        }

        // 通常の敵の出現処理
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= currentPhase.SpawnInterval)
        {
            SpawnEnemies(currentPhase);
            _spawnTimer = 0f;
        }
    }

    /// <summary>
    /// 通常の敵を出現させる処理
    /// </summary>
    /// <param name="phase">現在のフェーズ設定</param>
    void SpawnEnemies(Phase phase)
    {
        // 敵またはスポーン地点が設定されていなければ出現させない
        if (phase.EnemyPrefabs.Length == 0 || SpawnPoints.Length == 0)
        {
            return;
        }

        // 出現数をランダムに決定（最小～最大）
        int spawnCount = Random.Range(phase.MinSpawnCount, phase.MaxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            // 出現する敵と位置をランダムに選択
            GameObject enemy = phase.EnemyPrefabs[Random.Range(0, phase.EnemyPrefabs.Length)];
            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            // 敵を出現
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// ボスを出現させる処理
    /// </summary>
    /// <param name="bossPrefab">出現させるボスのプレハブ</param>
    void SpawnBoss(GameObject bossPrefab)
    {
        if (SpawnPoints.Length == 0) return;

        // 出現位置をランダムに選択
        Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

        // ボスを出現させる
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log("ボス出現！");
    }
}

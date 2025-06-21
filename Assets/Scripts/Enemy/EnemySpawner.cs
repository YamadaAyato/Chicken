using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // �e�t�F�[�Y�̐ݒ�N���X
    //[System.Serializable]�́AScriptableObject �� MonoBehaviour �̒��ŁA���G�ȃf�[�^�\�������������Ƃ�
    //���X�g��z��̗v�f�Ƃ��ēƎ��̃N���X��\���������Ƃ��B
    [System.Serializable]  
    public class Phase
    {
        public GameObject[] _enemyPrefabs;    // ���̃t�F�[�Y�ŏo������G�v���n�u
        public GameObject _bossPrefab;        // ���̃t�F�[�Y�ŏo��������{�X�̃v���n�u�inull�ŏo���Ȃ��j
        public bool _spawnBoss = false;       // ���̃t�F�[�Y�Ń{�X���o�������邩
        public float _spawnInterval = 3f;     // �G���X�|�[������Ԋu
        public int _minSpawnCount = 1;        // ��x�ɏo���G�̍ŏ���
        public int _maxSpawnCount = 3;        // ��x�ɏo���G�̍ő吔
    }

    public Phase[] _phases;                  // �e�t�F�[�Y�̐ݒ胊�X�g
    public float _phaseDuration = 15f;       // �e�t�F�[�Y�̒���
    public Transform[] _spawnPoints;         // �G�̏o���ʒu�@

    private int _currentPhaseIndex = 0;      // �t�F�[�Y�ԍ�
    private float _phaseTimer = 0f;          // �t�F�[�Y�̌o�ߎ���
    private float _spawnTimer = 0f;          // �G�̃X�|�[���p�^�C�}�[
    private bool _bossSpawned = false;       // ���̃t�F�[�Y�Ń{�X���o���ς݂�

    void Update()
    {
        _phaseTimer += Time.deltaTime;  //�t�F�[�Y�̌o�ߎ���

        // �t�F�[�Y�̎������Ԃ𒴂����玟�̃t�F�[�Y��
        if (_currentPhaseIndex < _phases.Length - 1 && _phaseTimer >= _phaseDuration)
        {
            _currentPhaseIndex++;
            _phaseTimer = 0f;
            _spawnTimer = 0f;
            _bossSpawned = false;  // �{�X�o���t���O�����Z�b�g
            Debug.Log($"�t�F�[�Y {_currentPhaseIndex + 1} �Ɉڍs");
        }

        Phase currentPhase = _phases[_currentPhaseIndex];

        // �{�X�̏o������
        if (currentPhase._spawnBoss && !_bossSpawned && currentPhase._bossPrefab != null)
        {
            SpawnBoss(currentPhase._bossPrefab);
            _bossSpawned = true;
        }

        // �ʏ�̓G�̏o������
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= currentPhase._spawnInterval)
        {
            SpawnEnemies(currentPhase);
            _spawnTimer = 0f;
        }
    }

    /// <summary>
    /// �ʏ�̓G���o�������鏈��
    /// </summary>
    /// <param name="phase">���݂̃t�F�[�Y�ݒ�</param>
    void SpawnEnemies(Phase phase)
    {
        // �G�܂��̓X�|�[���n�_���ݒ肳��Ă��Ȃ���Ώo�������Ȃ�
        if (phase._enemyPrefabs.Length == 0 || _spawnPoints.Length == 0) return;

        // �o�����������_���Ɍ���i�ŏ��`�ő�j
        int spawnCount = Random.Range(phase._minSpawnCount, phase._maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            // �o������G�ƈʒu�������_���ɑI��
            GameObject enemy = phase._enemyPrefabs[Random.Range(0, phase._enemyPrefabs.Length)];
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            // �G���o��
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// �{�X���o�������鏈��
    /// </summary>
    /// <param name="bossPrefab">�o��������{�X�̃v���n�u</param>
    void SpawnBoss(GameObject bossPrefab)
    {
        if (_spawnPoints.Length == 0) return;

        // �o���ʒu�������_���ɑI��
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        // �{�X���o��������
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log("�{�X�o���I");
    }
}

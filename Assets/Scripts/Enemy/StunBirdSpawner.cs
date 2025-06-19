using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("�X�|�[���ݒ�")]
    [SerializeField] private StunBird _stunBirdPrefab;  //�X�|�[��������G�̃v���n�u
    [SerializeField] private Transform[] _spawnPoints;�@�@//�o���ʒu

    [Header("�X�|�[���̊Ԋu")]
    [SerializeField] private float _minInterval = 1f;  //�o���̍ŏ��Ԋu
    [SerializeField] private float _maxInterval = 5f;  //�o���̍ő�Ԋu
    private float _spawntimer;

    void Start()
    {
        //_spawntimer�ɃX�|�[���Ԋu�̒l���烉���_���ɐ������o�����
        _spawntimer = Random.Range(_minInterval, _maxInterval);
    }

    void Update()
    {
        _spawntimer -= Time.deltaTime;

        //�X�|�[���^�C�}�[��0�ȉ��̎�
        if (_spawntimer <= 0)
        {
            Spawn();
            _spawntimer = Random.Range(_minInterval, _maxInterval);
        }
    }

    void Spawn()
    {
        //�z��̗v�f�����烉���_���ɐ����o���A���̐��̃|�C���g����
        int index = Random.Range(0, _spawnPoints.Length);
        Transform _spawnPoint = _spawnPoints[index];

        //�X�|�[���|�C���g�ɓG������
        StunBird enemy = Instantiate(_stunBirdPrefab, _spawnPoint.position, Quaternion.identity);
    }
}

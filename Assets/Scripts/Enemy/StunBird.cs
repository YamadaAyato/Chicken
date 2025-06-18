using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StunBird : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField] private float _moveSpeed = 5f;  //�O�i�X�s�[�h
    [SerializeField] private float _waveSpeed = 2f;  //�g�̑���
    [SerializeField] private float _waveHeight = 1f;  //�g�̍��� 
    public Vector3 direction = Vector3.right; // �ړ������i�E�ɐi�ށj

    [Header("�X�^���ݒ�")]
    [SerializeField] private float _stunDuration;  //�v���C���[�̃X�^������

    private Vector2 _startPosition;  //�����ʒu
    private float _waveOffset;  //�g�̊J�n�ʑ�


    // Start is called before the first frame update
    void Start()
    {
        //�����ʒu���L�^
       _startPosition = transform.position;
        // �T�C���g�̃X�^�[�g�ʒu�������_����
        _waveOffset = Random.Range(0f, Mathf.PI * 2f);
        // �����x�N�g���𐳋K���i���S�̂��߁j
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //�E�ւ̒��i�ړ�
        //�t���[�����[�g�Ɉˑ����Ȃ��������������邽�߂Ɏ��Ԃ�������
        Vector3 forwardMove = new Vector3(direction.x, direction.y, 0f) * _moveSpeed * Time.deltaTime;
        
        // �T�C���g�ŏ㉺�ɗh���悤��Y���W�����Z
        float wave = Mathf.Sin(Time.time * _waveSpeed + _waveOffset) *_waveHeight;
        
        // �㉺�ړ�����Time.deltaTime�Ŋ��炩�ɕ␳
        Vector3 waveOffsetY = new Vector3(0f, wave, 0f) * Time.deltaTime;
       
        // �������Ĉړ�
        transform.position += forwardMove + waveOffsetY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var stunnable = collision.gameObject.GetComponent<PlayerController>();
            stunnable.Stun(_stunDuration); // �w��b�������X�^��������

            Destroy(gameObject);
        }
    }
}

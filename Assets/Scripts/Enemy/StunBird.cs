using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBird : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;  //�O�i�X�s�[�h
    [SerializeField] private float _waveSpeed;  //�g�̑���
    [SerializeField] private float _waveHeight;�@ //�g�̍���
    [SerializeField] private float _stunDuration;  //�v���C���[�̃X�^������
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

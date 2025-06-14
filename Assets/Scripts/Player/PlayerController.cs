using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _egg;

    [Header("�ړ��ݒ�")]
    [SerializeField] private float _moveSpeed = 10f;�@//�ړ����x
    [SerializeField] private float _jumpforce = 10f;�@//�W�����v��
    [SerializeField] private float _maxSpeed = 50f;�@//���x���

    [Header("���ݒ�")]
    [SerializeField] private float _eggDropForce = 10f; //���𗎂Ƃ���
    [SerializeField] private float _fireInterval = 2f;�@//���𗎂Ƃ��Ԋu
    private Rigidbody2D _rb;
    bool IsGrounded = false;
    float _timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍv��
        _timer += Time.deltaTime;

        Move();

        //IsGrounded��true�̎���shift�������ꂽ�Ƃ��W�����v���\�b�h�����s
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        DropEgg();
    }
    private void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (_rb != null)
        {
            //���x��_maxSpeed�łȂ���Η͂�������
            if (Mathf.Abs(_rb.velocity.x) < _maxSpeed)
            {
                _rb.AddForce(Vector2.right * move * _moveSpeed);
            }
        }
    }

    private void Jump()
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            //�ݒu����̕ϐ���false��
            IsGrounded = false;
        }
    }

    private void DropEgg()
    {
        if (Input.GetButtonDown("Fire1") && _timer > _fireInterval)
        {
            //�}�Y���̈ʒu���痑�𐶐�
            GameObject egg = Instantiate(_egg,_muzzle.transform.position, Quaternion.identity);

            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            //���ɗ�����͂�������
            eggRb.AddForce(Vector2.down * _eggDropForce, ForceMode2D.Impulse);
            //���Ԍv�������Z�b�g
            _timer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //����ƐڐG���Ă���Ƃ��A�ڒn����̕ϐ���true��
        if (collision.gameObject.CompareTag("Scaffold"))
        {
            Debug.Log("�n�ʂɂ������Ă��܂�");
            IsGrounded = true;
        }
    }
}

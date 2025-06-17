using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private bool _IsStunned;  //�X�^�����Ă��邩
    private float _stuntimer;  // 

    // Update is called once per frame
    void Update()
    {
        if (_IsStunned)
        {
            _stuntimer -= Time.deltaTime;
            if (_stuntimer <= 0 )
            {
                _IsStunned = false;
                Debug.Log("�X�^������");
            }


        }
    }

    public void Stun(float _duration)
    {
        _IsStunned = true;
        _stuntimer = _duration;
        Debug.Log($"�X�^����{_duration}�b");
    }
}

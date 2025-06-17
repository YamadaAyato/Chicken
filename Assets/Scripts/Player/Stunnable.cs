using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private bool _IsStunned;  //スタンしているか
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
                Debug.Log("スタン解除");
            }


        }
    }

    public void Stun(float _duration)
    {
        _IsStunned = true;
        _stuntimer = _duration;
        Debug.Log($"スタン中{_duration}秒");
    }
}

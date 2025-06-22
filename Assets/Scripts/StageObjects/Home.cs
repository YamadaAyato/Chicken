using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{

    public static Home Instance { get; private set; }

    [SerializeField] public int _homeMaxHp = 5;
    private int _homeCurrentHp;

    
    void Start()
    {
        _homeCurrentHp = _homeMaxHp;
    }

    /// <summary>
    /// Enemy���ŌĂяo���ƂɃ_���[�W��^����
    /// </summary>
    /// <param name="damegeHome">Enemy��������</param>
    public void TakeDamegeHome(int damegeHome)
    {
        _homeCurrentHp -= damegeHome;
        //HP��0�ȉ��ɂȂ�����GameOver Scene�����[�h
        if (_homeCurrentHp <= 0)
        {
            _homeCurrentHp = 0;
            SceneManager.LoadScene("GameOver");
        }
    }
}

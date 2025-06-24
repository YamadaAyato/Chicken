using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{

    public static Home Instance { get; private set; }

    [Header("�Ƃ�HP�ݒ�")]
    [SerializeField] public int _homeMaxHp = 5;
    private int _homeCurrentHp;

    [Header("UI�֘A")]
    [SerializeField] private Image _hpFillImage;


    void Start()
    {
        _homeCurrentHp = _homeMaxHp;
        UpdateHpUI();   // �����\���̍X�V
    }

    /// <summary>
    /// Enemy���ŌĂяo���ƂɃ_���[�W��^����
    /// </summary>
    /// <param name="damegeHome">Enemy��������</param>
    public void TakeDamegeHome(int damegeHome)
    {
        _homeCurrentHp -= damegeHome;
        _homeCurrentHp = Mathf.Clamp(_homeCurrentHp, 0, _homeMaxHp);
        UpdateHpUI();

        //HP��0�ȉ��ɂȂ�����GameOver Scene�����[�h
        if (_homeCurrentHp <= 0)
        {
            _homeCurrentHp = 0;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateHpUI()
    {
        if (_hpFillImage != null)
        {
            float fillAmount = (float)_homeCurrentHp / _homeMaxHp;
            _hpFillImage.fillAmount = fillAmount;
        }
    }
}

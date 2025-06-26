using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{

    public static Home Instance { get; private set; }

    [Header("家のHP設定")]
    [SerializeField] public int _homeMaxHp = 5;
    private int _homeCurrentHp;

    [Header("UI関連")]
    [SerializeField] private Image _hpFillImage;

    [Header("効果音")]
    [SerializeField] private AudioClip _seClip;
    private AudioSource _audioSoirce;


    void Awake()
    {
        // シングルトン設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audioSoirce = GetComponent<AudioSource>();

        _homeCurrentHp = _homeMaxHp;
        UpdateHpUI();   // 初期表示の更新
    }

    /// <summary>
    /// Enemy側で呼び出し家にダメージを与える
    /// </summary>
    /// <param name="damegeHome">Enemy側から代入</param>
    public void TakeDamegeHome(int damegeHome)
    {
        _homeCurrentHp -= damegeHome;
        _homeCurrentHp = Mathf.Clamp(_homeCurrentHp, 0, _homeMaxHp);
        _audioSoirce.PlayOneShot(_seClip);
        UpdateHpUI();

        //HPが0以下になったらGameOver Sceneをロード
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
            Debug.Log("HPUIを更新");
            float fillAmount = (float)_homeCurrentHp / _homeMaxHp;
            _hpFillImage.fillAmount = fillAmount;
        }
    }
}

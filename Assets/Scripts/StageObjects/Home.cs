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
    /// Enemy側で呼び出し家にダメージを与える
    /// </summary>
    /// <param name="damegeHome">Enemy側から代入</param>
    public void TakeDamegeHome(int damegeHome)
    {
        _homeCurrentHp -= damegeHome;
        //HPが0以下になったらGameOver Sceneをロード
        if (_homeCurrentHp <= 0)
        {
            _homeCurrentHp = 0;
            SceneManager.LoadScene("GameOver");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UIの参照")]
    [SerializeField] private Text _scoreText;

    private int _score = 0;


    void Awake()
    {
        //一つだけ存在させる処理
        if (Instance == null)
        {
            Instance = this;  // 自分自身を代入（最初の1個）
        }
        else
        {
            Destroy(gameObject);  // 2個目以降は削除（重複防止）
        }
    }

    public void AddScore(int amount)
    {
        _score += amount;  //Enemy側から加算
        Debug.Log("スコア加算");
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
            Debug.Log($"スコア+{_score}");
        }
    }

}

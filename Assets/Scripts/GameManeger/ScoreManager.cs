using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } //どこからでも呼べる

    [Header("UIの参照")]
    [SerializeField] private Text _scoreText; 

    public int _score = 0;


    void Awake()
    {
        //一つだけ存在させる処理
        if (Instance == null)
        {
            Instance = this;  // 自分自身を代入（最初の1個）
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);  // 2個目以降は削除（重複防止）
        }
    }

    void Start()
    {
        //シーン開始時に探す
        //設計的にはよくない
        _scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        UpdateScoreText();
    }

    /// <summary>
    /// スコアを加算する処理
    /// </summary>
    /// <param name="amount">Enemy側から代入する値</param>
    public void AddScore(int amount)
    {
        _score += amount;  //Enemy側から加算
        Debug.Log("スコア加算");
        UpdateScoreText();
    }


    /// <summary>
    /// スコアを更新して表示する処理
    /// </summary>
    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
            Debug.Log($"スコア+{_score}");
        }
    }
}

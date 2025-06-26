using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text[] _resultText;
    [SerializeField] Text _yourScore;
    // Start is called before the first frame update
    void Start()
    {
        int _currentScore = ScoreManager.Instance._score;
        Debug.Log("現在のスコア: " + _currentScore);

        // 「あなたのスコア」を表示
        _yourScore.text = $"{_currentScore.ToString("D5")}";

        // _resultText[0].text = ScoreManager.Instance._score.ToString();

        List<int> scores = LoadScores();

        // 0点を除外してから追加（＝初期状態でも順位おかしくならない）
       // scores = scores.Where(s => s > 0).ToList();
       // scores.Add(_currentScore);

        // 新しいスコアを追加
        scores.Add(_currentScore);

        // スコアを降順でソート（高い順）
        scores = scores.OrderByDescending(s => s).ToList();

        // 上位5件だけ保存
        scores = scores.Take(5).ToList();

        // 保存
        SaveScores(scores);

        // 表示
        for (int i = 0; i < _resultText.Length; i++)
        {
            if (i < scores.Count)
            {
                _resultText[i].text = $"{i + 1}位 : {scores[i].ToString("D5")}";
            }
            else
            {
                _resultText[i].text = $"{i + 1}位 : --------";
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<int> LoadScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt($"HighScore{i}", 0));
        }
        return scores;
    }

    void SaveScores(List<int> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", scores[i]);
        }
        PlayerPrefs.Save();
    }
}

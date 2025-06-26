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
        Debug.Log("���݂̃X�R�A: " + _currentScore);

        // �u���Ȃ��̃X�R�A�v��\��
        _yourScore.text = $"{_currentScore.ToString("D5")}";

        // _resultText[0].text = ScoreManager.Instance._score.ToString();

        List<int> scores = LoadScores();

        // 0�_�����O���Ă���ǉ��i��������Ԃł����ʂ��������Ȃ�Ȃ��j
       // scores = scores.Where(s => s > 0).ToList();
       // scores.Add(_currentScore);

        // �V�����X�R�A��ǉ�
        scores.Add(_currentScore);

        // �X�R�A���~���Ń\�[�g�i�������j
        scores = scores.OrderByDescending(s => s).ToList();

        // ���5�������ۑ�
        scores = scores.Take(5).ToList();

        // �ۑ�
        SaveScores(scores);

        // �\��
        for (int i = 0; i < _resultText.Length; i++)
        {
            if (i < scores.Count)
            {
                _resultText[i].text = $"{i + 1}�� : {scores[i].ToString("D5")}";
            }
            else
            {
                _resultText[i].text = $"{i + 1}�� : --------";
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

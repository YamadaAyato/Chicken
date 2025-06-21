using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI�̎Q��")]
    [SerializeField] private Text _scoreText;

    private int _score = 0;


    void Awake()
    {
        //��������݂����鏈��
        if (Instance == null)
        {
            Instance = this;  // �������g�����i�ŏ���1�j
        }
        else
        {
            Destroy(gameObject);  // 2�ڈȍ~�͍폜�i�d���h�~�j
        }
    }

    public void AddScore(int amount)
    {
        _score += amount;  //Enemy��������Z
        Debug.Log("�X�R�A���Z");
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
            Debug.Log($"�X�R�A+{_score}");
        }
    }

}

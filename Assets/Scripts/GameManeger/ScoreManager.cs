using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } //�ǂ�����ł��Ăׂ�

    [Header("UI�̎Q��")]
    [SerializeField] private Text _scoreText; 

    public int _score = 0;


    void Awake()
    {
        //��������݂����鏈��
        if (Instance == null)
        {
            Instance = this;  // �������g�����i�ŏ���1�j
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);  // 2�ڈȍ~�͍폜�i�d���h�~�j
        }
    }

    void Start()
    {
        //�V�[���J�n���ɒT��
        //�݌v�I�ɂ͂悭�Ȃ�
        _scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        UpdateScoreText();
    }

    /// <summary>
    /// �X�R�A�����Z���鏈��
    /// </summary>
    /// <param name="amount">Enemy������������l</param>
    public void AddScore(int amount)
    {
        _score += amount;  //Enemy��������Z
        Debug.Log("�X�R�A���Z");
        UpdateScoreText();
    }


    /// <summary>
    /// �X�R�A���X�V���ĕ\�����鏈��
    /// </summary>
    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
            Debug.Log($"�X�R�A+{_score}");
        }
    }
}

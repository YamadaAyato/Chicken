using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI�̎Q��")]
    [SerializeField] private Text _scoreText;

    public int _score = 0;

    void Awake()
    {
        //�V���O���g��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �V�[���ύX���� UI ���Đڑ�
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �V�[�����ǂݍ��܂ꂽ�Ƃ��ɌĂ΂��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();

        if (scene.name == "InGame") 
        {
            ResetScore();
        }

        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        _score += amount;
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

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreText();
    }
}

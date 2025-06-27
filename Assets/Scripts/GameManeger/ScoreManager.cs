using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UIの参照")]
    [SerializeField] private Text _scoreText;

    public int _score = 0;

    void Awake()
    {
        //シングルトン
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // シーン変更時に UI を再接続
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // シーンが読み込まれたときに呼ばれる
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

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreText();
    }
}

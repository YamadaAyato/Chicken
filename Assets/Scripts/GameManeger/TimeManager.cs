using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public float _startTime = 0;
    [SerializeField] public float _currentTime;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startTime;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            _currentTime = 0;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        //時間がマイナスになったときに0以下にしないための安全処理
        float timeToShow = Mathf.Max(_currentTime, 0f);
        //FloorToIntは、小数点以下を切り捨て
        int minutes = Mathf.FloorToInt(timeToShow / 60);  
        int seconds = Mathf.FloorToInt(timeToShow % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text[] _resultText;
    // Start is called before the first frame update
    void Start()
    {
        _resultText[0].text = ScoreManager.Instance._score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

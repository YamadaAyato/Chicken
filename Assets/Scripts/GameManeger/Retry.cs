using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    /// <summary>
    /// ボタンを押したらタイトル画面に戻る
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

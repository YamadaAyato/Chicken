using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    /// <summary>
    /// �{�^������������^�C�g����ʂɖ߂�
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

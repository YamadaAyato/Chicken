using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    /// <summary>
    /// �N���b�N���ꂽ��InGame Scene�����[�h
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene("InGame");
    }

}

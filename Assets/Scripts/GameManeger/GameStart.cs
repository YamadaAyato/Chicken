using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    /// <summary>
    /// クリックされたらInGame Sceneをロード
    /// </summary>
    public void OnClick()
    {
        SceneManager.LoadScene("InGame");
    }

}

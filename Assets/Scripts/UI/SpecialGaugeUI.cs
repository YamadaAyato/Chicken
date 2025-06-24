using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialGaugeUI : MonoBehaviour
{
    [Header("ゲージ画像")]
    [SerializeField] private Image _gaugeImage;

    [Header("プレイヤー")]
    [SerializeField] private PlayerController _player;

    [Header("Ready表示用テキスト")]
    [SerializeField] private GameObject _readyText;

    private bool _wasReady = false;

    private void Update()
    {
        if (_player == null || _gaugeImage == null) return;  //nullチェック

        //ゲージの更新
        float ratio = _player.CurrentGaugeRatio;
        _gaugeImage.fillAmount = ratio;

        // Ready表示の管理
        if (ratio >= 1f)
        {
            if (!_wasReady)
            {
                ShowReadyText(true);
                _wasReady = true;
            }
        }
        else
        {
            if (_wasReady)
            {
                ShowReadyText(false);
                _wasReady = false;
            }
        }
    }

    private void ShowReadyText(bool show)
    {
        if (_readyText != null)
        {
            _readyText.SetActive(show);
        }
    }
}

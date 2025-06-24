using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialGaugeUI : MonoBehaviour
{
    [Header("�Q�[�W�摜")]
    [SerializeField] private Image _gaugeImage;

    [Header("�v���C���[")]
    [SerializeField] private PlayerController _player;

    [Header("Ready�\���p�e�L�X�g")]
    [SerializeField] private GameObject _readyText;

    private bool _wasReady = false;

    private void Update()
    {
        if (_player == null || _gaugeImage == null) return;  //null�`�F�b�N

        //�Q�[�W�̍X�V
        float ratio = _player.CurrentGaugeRatio;
        _gaugeImage.fillAmount = ratio;

        // Ready�\���̊Ǘ�
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToUIManager : MonoBehaviour
{
    [SerializeField] GameObject _howToPanel;

    public void ToggleHowToPanel()
    {
        bool isActive = _howToPanel.activeSelf;
        _howToPanel.SetActive(!isActive); // �\���E��\����؂�ւ�
    }

    public void CloseHowToPanel()
    {
        _howToPanel.SetActive(false);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTabHandler : MonoBehaviour
{
    [SerializeField] Button storeButton;
    [SerializeField] Button storeCloseButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button settingsCloseButton;

    [SerializeField] RectTransform StorePanel;
    [SerializeField] RectTransform MainMenuPanel;
    [SerializeField] RectTransform SettingsPanel;

    private void Start()
    {
        storeButton.onClick.AddListener(OpenStorePanel);
        storeCloseButton.onClick.AddListener(CloseStorePanel);

        settingsButton.onClick.AddListener(OpenSettingsPanel);
        settingsCloseButton.onClick.AddListener(CloseSettingsPanel);
    }

    void OpenStorePanel()
    {
        SoundManager.instance.PlayButtonPressedFX();
        MainMenuPanel.DOLocalMoveX(1080f, .5f).SetEase(Ease.OutCirc);
        StorePanel.gameObject.SetActive(true);
        StorePanel.DOLocalMoveX(0f, .5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            MainMenuPanel.gameObject.SetActive(false);
            //MainMenuPanel.anchoredPosition = new Vector2(-1080f, .5f);
        });
    }

    void CloseStorePanel()
    {
        SoundManager.instance.PlayButtonPressedFX();
        MainMenuPanel.gameObject.SetActive(true);
        MainMenuPanel.DOLocalMoveX(0f, .5f).SetEase(Ease.OutCirc);
        StorePanel.DOLocalMoveX(-1080f, .5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            StorePanel.gameObject.SetActive(false);
            //StorePanel.anchoredPosition = new Vector2(-1080f, 0f);
        });
    }

    void OpenSettingsPanel()
    {
        SoundManager.instance.PlayButtonPressedFX();
        MainMenuPanel.DOLocalMoveX(-1080f, .5f).SetEase(Ease.OutCirc);
        SettingsPanel.gameObject.SetActive(true);
        SettingsPanel.DOLocalMoveX(0f, .5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            MainMenuPanel.gameObject.SetActive(false);
            //MainMenuPanel.anchoredPosition = new Vector2(-1080f, .5f);
        });
    }

    void CloseSettingsPanel()
    {
        SoundManager.instance.PlayButtonPressedFX();
        MainMenuPanel.gameObject.SetActive(true);
        MainMenuPanel.DOLocalMoveX(0f, .5f).SetEase(Ease.OutCirc);
        SettingsPanel.DOLocalMoveX(1080f, .5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            SettingsPanel.gameObject.SetActive(false);
            //StorePanel.anchoredPosition = new Vector2(-1080f, 0f);
        });
    }
}

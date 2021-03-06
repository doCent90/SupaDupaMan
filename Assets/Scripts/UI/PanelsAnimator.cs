using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PanelsAnimator : MonoBehaviour
{
    [SerializeField] private GameWin _gameWin;
    [SerializeField] private Button _tapToStart;

    private Shop _shop;
    private YouWin _youWinText;
    private ButtonsUI _buttonsUI;
    private SettingsPanel _settings;
    private CanvasGroup _shopPanel;
    private CanvasGroup _youWinPanel;
    private CanvasGroup _settingsPanel;

    private const float Duration = 0.5f;
    private const float FullAlpha = 1f;
    private const float ZeroAlpha = 0;
    private const float Delay = 1.5f;

    private void OnEnable()
    {
        if (_gameWin == null || _tapToStart == null)
            throw new InvalidOperationException();

        _youWinText = GetComponentInChildren<YouWin>();
        _youWinPanel = _youWinText.GetComponent<CanvasGroup>();

        _shop = GetComponentInChildren<Shop>();
        _shopPanel = _shop.GetComponent<CanvasGroup>();

        _settings = GetComponentInChildren<SettingsPanel>();
        _settingsPanel = _settings.GetComponent<CanvasGroup>();

        _buttonsUI = GetComponentInChildren<ButtonsUI>();

        _gameWin.Won += OnGameWon;
        _buttonsUI.ShopClicked += OnShopButtonClicked;
        _buttonsUI.SettingsClicked += OnSettingsButtonClicked;
    }

    private void OnDisable()
    {
        _gameWin.Won -= OnGameWon;
        _buttonsUI.ShopClicked -= OnShopButtonClicked;
        _buttonsUI.SettingsClicked -= OnSettingsButtonClicked;
    }

    private void OnGameWon()
    {
        var tweenFade = _youWinPanel.DOFade(FullAlpha, Duration * 2).SetDelay(Delay);
    }

    private void OnShopButtonClicked(bool isOpen)
    {
        SetActivePanel(isOpen, _shopPanel);
    }

    private void OnSettingsButtonClicked(bool isOpen)
    {
        SetActivePanel(isOpen, _settingsPanel);
    }

    private void SetActivePanel(bool isOpen, CanvasGroup panel)
    {
        if (isOpen)
        {
            var tweenFadeOn = panel.DOFade(FullAlpha, Duration);
            tweenFadeOn.OnComplete(GameTimeStop);
            _tapToStart.interactable = false;
        }
        else
        {
            GameTimeGo();
            var tweenFadeOff = panel.DOFade(ZeroAlpha, Duration);
            _tapToStart.interactable = true;
        }

        panel.interactable = isOpen;
        panel.blocksRaycasts = isOpen;
    }

    private void GameTimeGo()
    {
        Time.timeScale = 1;
    }
    private void GameTimeStop()
    {
        Time.timeScale = 0;
    }
}

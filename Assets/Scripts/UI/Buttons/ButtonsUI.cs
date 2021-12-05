using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private StartGame _game;
    [SerializeField] private LevelsLoader _loadLevel;
    [SerializeField] private PlayerRotater _playerRotater;
    [SerializeField] private SoundsFXSettings _soundMaster;
    [Header("Shop")]
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _closeShop;
    [Header("Settings")]
    [SerializeField] private Button _openOptions;
    [SerializeField] private Button _closeOptions;
    [Header("Sound")]
    [SerializeField] private Button _onSoundButton;
    [SerializeField] private Button _offSoundButton;

    private CurrentCoinsViewer _coinsViewer;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string Level = "level";
    private const string TimeGame = "time_spent";

    public bool IsPanelOpen { get; private set; } = false;

    public event UnityAction Clicked;
    public event UnityAction StartButtonClicked;
    public event UnityAction ContinueButtonClicked;

    public event UnityAction<bool> ShopClicked;
    public event UnityAction<bool> SettingsClicked;

    public void TapToStart()
    {
        StartButtonClicked?.Invoke();
        Clicked?.Invoke();
    }

    public void StartCurrentLevel()
    {
        _game.StartLevel();
    }

    public void Continue()
    {
        ContinueButtonClicked?.Invoke();
        Clicked?.Invoke();
    }

    public void NextLevel()
    {
        _loadLevel.LoadNext();
        _isLevelDone = true;

        SetAmplitudeValue(TimeGame, (int)_elapsedTime);
        SetAmplitudeValue(Level, _loadLevel.Level);
    }

    public void OpenSettings()
    {
        IsPanelOpen = true;
        _playerRotater.enabled = false;

        _openShop.gameObject.SetActive(false);
        _coinsViewer.gameObject.SetActive(false);
        _openOptions.gameObject.SetActive(false);
        _closeOptions.gameObject.SetActive(true);

        SettingsClicked?.Invoke(true);
        Clicked?.Invoke();
    }

    public void CloseSettings()
    {
        IsPanelOpen = false;

        if(_game.IsPlaying)
            _playerRotater.enabled = true;

        _openShop.gameObject.SetActive(true);
        _openOptions.gameObject.SetActive(true);
        _coinsViewer.gameObject.SetActive(true);
        _closeOptions.gameObject.SetActive(false);

        SettingsClicked?.Invoke(false);
        Clicked?.Invoke();
    }

    public void EnableSound()
    {
        _soundMaster.EnableSound();

        _onSoundButton.gameObject.SetActive(false);
        _offSoundButton.gameObject.SetActive(true);
        Clicked?.Invoke();
    }

    public void DisableSound()
    {
        _soundMaster.DisableSound();

        _onSoundButton.gameObject.SetActive(true);
        _offSoundButton.gameObject.SetActive(false);
        Clicked?.Invoke();
    }

    public void OpenShop()
    {
        IsPanelOpen = true;
        _playerRotater.enabled = false;

        _openShop.gameObject.SetActive(false);
        _closeShop.gameObject.SetActive(true);
        _coinsViewer.gameObject.SetActive(false);
        _openOptions.gameObject.SetActive(false);

        ShopClicked?.Invoke(true);
        Clicked?.Invoke();
    }

    public void CloseShop()
    {
        if (_game.IsPlaying)
            _playerRotater.enabled = true;

        _openShop.gameObject.SetActive(true);
        _closeShop.gameObject.SetActive(false);
        _coinsViewer.gameObject.SetActive(true);
        _openOptions.gameObject.SetActive(true);

        ShopClicked?.Invoke(false);
        Clicked?.Invoke();
    }

    private void Awake()
    {
        if (_game == null || _loadLevel == null || _playerRotater == null
            || _soundMaster == null || _openShop == null || _closeShop == null
            || _openOptions == null || _closeOptions == null
            || _onSoundButton == null || _offSoundButton == null)
        {
            throw new InvalidOperationException();
        }

        _coinsViewer = GetComponentInChildren<CurrentCoinsViewer>();

        Init();
    }

    private void SetAmplitudeValue(string label, int value)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {label, value.ToString()}
        };

        Amplitude.Instance.logEvent(label, dictionary);
    }

    private void Init()
    {
        _closeOptions.gameObject.SetActive(false);

        if (_soundMaster.IsSoundEnable)
        {
            _onSoundButton.gameObject.SetActive(true);
            _offSoundButton.gameObject.SetActive(false);
        }
        else
        {
            _onSoundButton.gameObject.SetActive(false);
            _offSoundButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}

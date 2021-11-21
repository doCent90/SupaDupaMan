using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ButtonsUI : MonoBehaviour
{    
    [Header("Shop")]
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _closeShop;
    [Header("Settings")]
    [SerializeField] private Button _openOptions;
    [SerializeField] private Button _closeOptions;
    [Header("Sound")]
    [SerializeField] private Button _onSoundButton;
    [SerializeField] private Button _offSoundButton;

    private Dictionary<string, int> _levelComplete = new Dictionary<string, int>();
    private Dictionary<string, int> _timeSpent = new Dictionary<string, int>();

    private CurrentCoinsViewer _coinsViewer;
    private SoundsFXSettings _soundMaster;
    private PlayerRotater _playerRotater;
    private LevelsLoader _loadLevel;
    private StartGame _game;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string Level = "level";
    private const string LevelComplete = "level_complete";
    private const string TimeGame = "time_spent";
    private const string TimeSpent = "time_spent_lvl_complete";

    public bool IsPanelOpen { get; private set; } = false;

    public event UnityAction Clicked;
    public event UnityAction StartButtonClicked;
    public event UnityAction ContinueButtonClicked;

    public event UnityAction<bool> ShopClicked;
    public event UnityAction<bool> SettingsClicked;

    public void TapToStart()
    {
        StartButtonClicked?.Invoke();
    }

    public void StartCurrentLevel()
    {
        _game.StartLevel();
    }

    public void Continue()
    {
        ContinueButtonClicked?.Invoke();
    }

    public void NextLevel()
    {
        _loadLevel.LoadNext();
        Clicked?.Invoke();

        _isLevelDone = true;

        _timeSpent.Add(TimeGame, (int)_elapsedTime);
        _levelComplete.Add(Level, _loadLevel.Level);

        Amplitude.Instance.logEvent(TimeSpent, (IDictionary<string, object>)_timeSpent);
        Amplitude.Instance.logEvent(LevelComplete, (IDictionary<string, object>)_levelComplete);
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
        IsPanelOpen = false;
        _playerRotater.enabled = true;

        _openShop.gameObject.SetActive(true);
        _closeShop.gameObject.SetActive(false);
        _coinsViewer.gameObject.SetActive(true);
        _openOptions.gameObject.SetActive(true);

        ShopClicked?.Invoke(false);
        Clicked?.Invoke();
    }

    private void OnEnable()
    {
        _game = FindObjectOfType<StartGame>();
        _loadLevel = FindObjectOfType<LevelsLoader>();
        _playerRotater = FindObjectOfType<PlayerRotater>();
        _soundMaster = FindObjectOfType<SoundsFXSettings>();
        _coinsViewer = FindObjectOfType<CurrentCoinsViewer>();

        Init();
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

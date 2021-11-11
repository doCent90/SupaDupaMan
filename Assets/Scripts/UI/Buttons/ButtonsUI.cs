using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonsUI : MonoBehaviour
{    
    [Header("Buttons")]
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonContinue;
    [Header("Shop")]
    [SerializeField] private GameObject _shop;
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _closeShop;
    [Header("Settings")]
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private Button _openOptions;
    [SerializeField] private Button _closeOptions;
    [Header("Sound")]
    [SerializeField] private Button _onSoundButton;
    [SerializeField] private Button _offSoundButton;

    private CurrentCoinsViewer _coinsViewer;
    private SoundsFXSettings _soundMaster;
    private PlayerRotater _playerRotater;
    private LevelsLoader _loadLevel;
    private GameWin _gameWin;
    private StartGame _game;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string LevelComplete = "level_complete";
    private const string TimeSpent = "time_spent_lvl_complete";

    public bool IsPanelOpen { get; private set; } = false;

    public event UnityAction Clicked;

    public void StartCurrentLevel()
    {
        _game.StartLevel();
        _buttonStart.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        _buttonContinue.gameObject.SetActive(false);
        _loadLevel.LoadNext();
        Clicked?.Invoke();

        _isLevelDone = true;

        Amplitude.Instance.logEvent(LevelComplete, _loadLevel.Level);
        Amplitude.Instance.logEvent(TimeSpent, (int)_elapsedTime);
    }

    public void OpenSettings()
    {
        IsPanelOpen = true;
        Time.timeScale = 0;
        _playerRotater.enabled = false;

        _panelOptions.SetActive(true);
        _openShop.gameObject.SetActive(false);
        _coinsViewer.gameObject.SetActive(false);
        _openOptions.gameObject.SetActive(false);
        _closeOptions.gameObject.SetActive(true);
        Clicked?.Invoke();
    }

    public void CloseSettings()
    {
        IsPanelOpen = false;
        Time.timeScale = 1;
        _playerRotater.enabled = true;

        _panelOptions.SetActive(false);
        _openShop.gameObject.SetActive(true);
        _openOptions.gameObject.SetActive(true);
        _coinsViewer.gameObject.SetActive(true);
        _closeOptions.gameObject.SetActive(false);
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
        Time.timeScale = 0;
        _playerRotater.enabled = false;

        _shop.SetActive(true);
        _openShop.gameObject.SetActive(false);
        _closeShop.gameObject.SetActive(true);
        _coinsViewer.gameObject.SetActive(false);
        _openOptions.gameObject.SetActive(false);
    }

    public void CloseShop()
    {
        IsPanelOpen = false;
        Time.timeScale = 1;
        _playerRotater.enabled = true;

        _shop.SetActive(false);
        _openShop.gameObject.SetActive(true);
        _closeShop.gameObject.SetActive(false);
        _coinsViewer.gameObject.SetActive(true);
        _openOptions.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _game = FindObjectOfType<StartGame>();
        _gameWin = FindObjectOfType<GameWin>();
        _loadLevel = FindObjectOfType<LevelsLoader>();
        _playerRotater = FindObjectOfType<PlayerRotater>();
        _soundMaster = FindObjectOfType<SoundsFXSettings>();
        _coinsViewer = FindObjectOfType<CurrentCoinsViewer>();

        _gameWin.Win += ShowContinueButton;

        Init();
    }

    private void OnDisable()
    {
        _gameWin.Win -= ShowContinueButton;
    }

    private void Init()
    {
        _panelOptions.SetActive(false);
        _closeOptions.gameObject.SetActive(false);
        _buttonContinue.gameObject.SetActive(false);

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

    private void ShowContinueButton()
    {
        _buttonContinue.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}
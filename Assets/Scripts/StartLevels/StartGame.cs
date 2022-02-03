using System;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : AmplitudeWriter
{
    [SerializeField] private Player _player;

    private PlayerMover _playerMover;
    private ObjectsSelector _objectsSelector;

    private int _countStartSessions = 0;

    private const string Coins = "Coins";
    private const string RegDay = "reg_day";
    private const string GameStart = "game_start";
    private const string GameStartCount = "count";
    private const string CountDaysGame = "days_in_game";
    private const string CountSessions = "CountSessions";

    public bool IsPlaying { get; private set; } = false;

    public event UnityAction Started;

    public void StartLevel()
    {
        Started?.Invoke();
        IsPlaying = true;

        _playerMover.enabled = true;
        _objectsSelector.enabled = true;
    }

    private void OnEnable()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(StartGame));

        _playerMover = _player.GetComponent<PlayerMover>();
        _objectsSelector = _player.GetComponentInChildren<ObjectsSelector>();
    }

    private void Start()
    {
        InitAmplitude();
        SetRegDay();
        SetDaysInGame();
        SetCountSessions();
    }

    private void SetCountSessions()
    {
        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        PlayerPrefs.SetInt(CountSessions, _countStartSessions);

        SetAmplitudeValue(GameStart, _countStartSessions, GameStartCount);
    }

    private void SetDaysInGame()
    {
        int days = PlayerPrefs.GetInt(CountDaysGame);
        days++;

        PlayerPrefs.SetInt(CountDaysGame, days);
        SetAmplitudeValue(CountDaysGame, days);
    }

    private void SetRegDay()
    {
        int False = 0;
        int True = 1;

        DateTime dateTime = DateTime.Today;

        if (True != PlayerPrefs.GetInt(RegDay))
        {
            return;
        }
        else
        {
            string date = dateTime.ToString();
            SetAmplitudeValue(RegDay, date);
        }

        if (PlayerPrefs.GetInt(RegDay) == False)
            PlayerPrefs.SetInt(RegDay, True);
    }
}

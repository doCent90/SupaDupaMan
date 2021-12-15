using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Player _player;

    private PlayerMover _playerMover;
    private ObjectsSelector _objectsSelector;

    private int _countStartSessions = 0;

    private const string Coins = "Coins";
    private const string Key = "b2757e20bee8ecd447b0ed8c368abd50";
    private const string CountSessions = "CountSessions";
    private const string CountDaysGame = "days_in_game";
    private const string Days = "days";
    private const string GameStart = "game_start";
    private const string GameStartCount = "count";
    private const string RegDay = "reg_day";

    public bool IsPlaying { get; private set; } = false;

    public event UnityAction Started;

    public void StartLevel()
    {
        Started?.Invoke();
        IsPlaying = true;

        _playerMover.enabled = true;
        _objectsSelector.enabled = true;
    }

    private void Awake()
    {
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

    private void InitAmplitude()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(Key);
    }

    private void SetAmplitudeValue(string label, int value)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {label, value.ToString()}
        };

        Amplitude.Instance.logEvent(label, dictionary);
    }

    private void SetAmplitudeValue(string label, string value)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {label, value}
        };

        Amplitude.Instance.logEvent(label, dictionary);
    }

    private void SetCountSessions()
    {
        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        PlayerPrefs.SetInt(CountSessions, _countStartSessions);

        SetAmplitudeValue(GameStartCount, _countStartSessions);
    }

    private void SetDaysInGame()
    {
        var days = PlayerPrefs.GetInt(CountDaysGame);
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

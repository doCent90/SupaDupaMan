using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    private Player _player;
    private PlayerMover _playerMover;
    private ObjectsSelector _objectsSelector;
    private Dictionary<string, int> _gameStart = new Dictionary<string, int>();
    private Dictionary<string, int> _daysInGame = new Dictionary<string, int>();
    private Dictionary<string, int> _registrationDay = new Dictionary<string, int>();

    private int _countStartSessions = 0;

    private const string Coins = "Coins";

    private const string Key = "b2757e20bee8ecd447b0ed8c368abd50";
    private const string CountSessions = "CountSessions";
    private const string CountDaysGame = "days_in_game";
    private const string Days = "days";
    private const string GameStart = "game_start";
    private const string GameStartCount = "count";
    private const string RegDay = "reg_day";

    public event UnityAction Started;

    public void StartLevel()
    {
        Started?.Invoke();

        _playerMover.enabled = true;
        _objectsSelector.enabled = true;
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt(Coins, 5000);
        Debug.Log("Added Coins Default");

        _player = FindObjectOfType<Player>();
        _playerMover = _player.GetComponent<PlayerMover>();
        _objectsSelector = _player.GetComponentInChildren<ObjectsSelector>();
    }

    private void Start()
    {
        Init();
        SetRegDay();
        SetDaysInGame();
        SetCountSessions();
    }

    private void Init()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(Key);
    }

    private void SetCountSessions()
    {
        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        _gameStart.Add(GameStartCount, _countStartSessions);

        PlayerPrefs.SetInt(CountSessions, _countStartSessions);
        Amplitude.Instance.logEvent(GameStart, (IDictionary<string, object>)_gameStart);
    }

    private void SetDaysInGame()
    {
        var days = PlayerPrefs.GetInt(CountDaysGame);
        days++;

        _daysInGame.Add(Days, days);

        PlayerPrefs.SetInt(CountDaysGame, days);
        Amplitude.Instance.logEvent(CountDaysGame, (IDictionary<string, object>)_daysInGame);
    }

    private void SetRegDay()
    {
        int False = 0;
        int True = 1;

        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;

        if (True != PlayerPrefs.GetInt(RegDay))
            return;
        else
        {
            _registrationDay.Add(RegDay, day + month + year);
            Amplitude.Instance.logEvent(RegDay, (IDictionary<string, object>)_registrationDay);
        }

        if (PlayerPrefs.GetInt(RegDay) == False)
            PlayerPrefs.SetInt(RegDay, True);
    }
}

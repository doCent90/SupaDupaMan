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
    private Dictionary<string, string> _registrationDay = new Dictionary<string, string>();

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

    private void SetCountSessions()
    {
        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        _gameStart.Add(GameStartCount, _countStartSessions);
        PlayerPrefs.SetInt(CountSessions, _countStartSessions);

        IDictionary<string, int> gameStart = _gameStart;
        Amplitude.Instance.logEvent(GameStart, gameStart);
    }

    private void SetDaysInGame()
    {
        var days = PlayerPrefs.GetInt(CountDaysGame);
        days++;

        _daysInGame.Add(Days, days);
        PlayerPrefs.SetInt(CountDaysGame, days);

        IDictionary<string, int> daysInGame = _daysInGame;
        Amplitude.Instance.logEvent(CountDaysGame, daysInGame);
    }

    private void SetRegDay()
    {
        int False = 0;
        int True = 1;

        DateTime dateTime = DateTime.Today;

        if (True != PlayerPrefs.GetInt(RegDay))
            return;
        else
        {
            _registrationDay.Add(RegDay, dateTime.ToString());

            IDictionary<string, string> registrationDay = _registrationDay;
            Amplitude.Instance.logEvent(RegDay, registrationDay);
        }

        if (PlayerPrefs.GetInt(RegDay) == False)
            PlayerPrefs.SetInt(RegDay, True);
    }
}

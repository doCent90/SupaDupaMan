using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    private YouLose _loseText;
    private GameLevelsLoader _gameLevelsLoader;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string Fail = "fail";
    private const string TimeSpent = "time_spent_fail";

    public event UnityAction Defeated;

    private void OnEnable()
    {
        _loseText = FindObjectOfType<YouLose>();
        _gameLevelsLoader = FindObjectOfType<GameLevelsLoader>();

        _loseText.gameObject.SetActive(false);
    }

    private void LoseLevel()
    {
        Defeated?.Invoke();

        Amplitude.Instance.logEvent(Fail, _gameLevelsLoader.Level);
        Amplitude.Instance.logEvent(TimeSpent, (int)_elapsedTime);
    }

    private void Update()
    {
        if (!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}

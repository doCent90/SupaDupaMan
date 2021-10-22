using UnityEngine;
using UnityEngine.Events;

public class GameOverField : MonoBehaviour
{
    private YouLose _loseText;
    private BackGroundMover _groundMover;
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
        _groundMover = GetComponentInParent<BackGroundMover>();

        _loseText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            var enemies = FindObjectsOfType<EnemyMover>();
            foreach (var stickman in enemies)
            {
                stickman.enabled = false;
            }

            Defeated?.Invoke();
            _groundMover.enabled = false;
            _loseText.gameObject.SetActive(true);

            _isLevelDone = true;

            Amplitude.Instance.logEvent(Fail, _gameLevelsLoader.Level);
            Amplitude.Instance.logEvent(TimeSpent, (int)_elapsedTime);
        }
    }

    private void Update()
    {
        if (!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}

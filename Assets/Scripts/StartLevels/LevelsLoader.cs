using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.Events;

public class LevelsLoader : MonoBehaviour
{
    [SerializeField] protected int _levelIndex;

    private const int FirstLevel = 1;
    private const string LevelDone = "LevelDone";
    private const string LastLevel = "last_level";
    private const string LevelStart = "level_start";

    public int Level => _levelIndex;

    public event UnityAction<int> LevelStarted;
    public event UnityAction<int> LevelFinished;

    public void LoadNext()
    {
        int numberLevel = _levelIndex;
        numberLevel++;

        LoadScene(numberLevel);
        SetLevelFinishPrefsValue(numberLevel);
    }

    public void Retry()
    {
        LoadScene(_levelIndex);
    }

    private void OnEnable()
    {
        var level = PlayerPrefs.GetInt(LevelDone);

        if(level <= FirstLevel)
            PlayerPrefs.SetInt(LevelDone, FirstLevel);

        LevelStarted?.Invoke(_levelIndex);
    }

    private void OnDisable()
    {
        LevelFinished?.Invoke(_levelIndex);
    }

    private void Start()
    {
        var currentLevel = PlayerPrefs.GetInt(LevelDone);

        if (currentLevel > _levelIndex)
            LoadScene(currentLevel);

        Amplitude.Instance.logEvent(LevelStart, currentLevel);
        Amplitude.Instance.logEvent(LastLevel, currentLevel);
    }

    private void LoadScene(int numberLevel)
    {
        switch (numberLevel)
        {
            case 1:
                LVL1.Load();
                break;
            case 2:
                LVL2.Load();
                break;
        }
    }

    private void SetLevelFinishPrefsValue(int numberLevel)
    {
        var level = PlayerPrefs.GetInt(LevelDone);

        if(level < numberLevel)
        {
            PlayerPrefs.SetInt(LevelDone, numberLevel);
        }
    }
}

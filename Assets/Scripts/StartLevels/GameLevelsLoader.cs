using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.Events;

public class GameLevelsLoader : MonoBehaviour
{
    [SerializeField] protected int _levelIndex;

    private const string LevelDone = "LevelDone";
    private const string LevelStart = "level_start";
    private const string LastLevel = "last_level";
    private const int FirstLevel = 1;

    public int Level => _levelIndex;

    public event UnityAction<int> StartLevel;
    public event UnityAction<int> EndLevel;

    public void LoadNext()
    {
        int numberLevel = _levelIndex;
        numberLevel++;

        LoadScene(numberLevel);
        SetLevelDoneValue(numberLevel);
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

        StartLevel?.Invoke(_levelIndex);
    }

    private void OnDisable()
    {
        EndLevel?.Invoke(_levelIndex);
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
        }
    }

    private void SetLevelDoneValue(int numberLevel)
    {
        var level = PlayerPrefs.GetInt(LevelDone);

        if(level < numberLevel)
        {
            PlayerPrefs.SetInt(LevelDone, numberLevel);
        }
    }
}

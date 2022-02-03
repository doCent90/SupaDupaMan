using UnityEngine;
using IJunior.TypedScenes;
using System.Collections.Generic;
using System;

public class LevelsLoader : AmplitudeWriter
{
    [SerializeField] protected int _levelIndex;

    private const int FirstLevel = 1;
    private const string LevelDone = "LevelDone";
    private const string LastLevel = "last_level";
    private const string LevelStart = "level_start";

    public int Level => _levelIndex;

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
        if (_levelIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(LevelsLoader));

        int level = PlayerPrefs.GetInt(LevelDone);

        if(level <= FirstLevel)
            PlayerPrefs.SetInt(LevelDone, FirstLevel);
    }

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt(LevelDone);

        if (currentLevel > _levelIndex)
            LoadScene(currentLevel);

        SetAmplitudeValue(LevelStart, currentLevel);
        SetAmplitudeValue(LastLevel, currentLevel);
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
            case 3:
                LVL3.Load();
                break;
            case 4:
                LVL4.Load();
                break;
            case 5:
                LVL5.Load();
                break;
            case 6:
                LVL6.Load();
                break;
            case 7:
                LVL7.Load();
                break;
            case 8:
                LVL8.Load();
                break;
            case 9:
                LVL9.Load();
                break;
            case 10:
                LVL10.Load();
                break;
            default:
                {
                    PlayerPrefs.SetInt(LevelDone, _levelIndex);
                    LVL5.Load();
                }
                break;
        }
    }

    private void SetLevelFinishPrefsValue(int numberLevel)
    {
        int level = PlayerPrefs.GetInt(LevelDone);

        if(level < numberLevel)
        {
            PlayerPrefs.SetInt(LevelDone, numberLevel);
        }
    }
}

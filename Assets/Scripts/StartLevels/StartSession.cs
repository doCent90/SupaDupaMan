using UnityEngine;
using IJunior.TypedScenes;

public class StartSession : AmplitudeWriter
{
    private const string SessionCount = "session_count";

    private void Awake()
    {
        int sessionCount = PlayerPrefs.GetInt(SessionCount);

        if (sessionCount <= 0)
            PlayerPrefs.SetInt(SessionCount, 0);

        if (sessionCount != 0)
        {
            sessionCount++;
            PlayerPrefs.SetInt(SessionCount, sessionCount);
        }

        LVL1.Load();

        SetAmplitudeValue(SessionCount, sessionCount);
    }
}

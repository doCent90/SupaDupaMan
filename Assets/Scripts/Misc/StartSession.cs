using UnityEngine;
using IJunior.TypedScenes;

public class StartSession : MonoBehaviour
{
    private const string SessionCount = "Session_Count";

    private void Awake()
    {
        int sessionCount = PlayerPrefs.GetInt(SessionCount);

        if (sessionCount != 0)
        {
            sessionCount++;
            PlayerPrefs.SetInt(SessionCount, sessionCount);
        }

        LVL1.Load();
    }

    private void Init()
    {
        int sessionCount = PlayerPrefs.GetInt(SessionCount);

        if (sessionCount <= 0)
            PlayerPrefs.SetInt(SessionCount, 0);
    }
}

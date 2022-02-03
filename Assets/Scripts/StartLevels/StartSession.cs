using UnityEngine;
using IJunior.TypedScenes;

public class StartSession : MonoBehaviour
{
    private const string Coins = "Coins";
    private const string SessionCount = "Session_Count";

    private void Awake()
    {
        PlayerPrefs.SetInt(Coins, 0);

        int sessionCount = PlayerPrefs.GetInt(SessionCount);

        if (sessionCount <= 0)
            PlayerPrefs.SetInt(SessionCount, 0);

        if (sessionCount != 0)
        {
            sessionCount++;
            PlayerPrefs.SetInt(SessionCount, sessionCount);
        }

        LVL1.Load();
    }
}

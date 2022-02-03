using UnityEngine;
using System.Collections.Generic;

public abstract class AmplitudeWriter : MonoBehaviour
{
    private const string Key = "b2757e20bee8ecd447b0ed8c368abd50";

    protected void SetAmplitudeValue(string label, int value, string type = null)
    {
        if (type != null)
            label = $"{label} {type}";

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {label, value.ToString()}
        };

        Amplitude.Instance.logEvent(label, dictionary);
    }

    protected void SetAmplitudeValue(string label, string value, string type = null)
    {
        if (type != null)
            label = $"{label} {type}";

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {label, value}
        };

        Amplitude.Instance.logEvent(label, dictionary);
    }

    protected void InitAmplitude()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(Key);
    }
}

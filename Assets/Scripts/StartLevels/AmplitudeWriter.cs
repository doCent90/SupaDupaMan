using UnityEngine;
using System.Collections.Generic;

public abstract class AmplitudeWriter : MonoBehaviour
{
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
}

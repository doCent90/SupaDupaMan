using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const float Delay = 0.6f;

    private void OnEnable()
    {
        Destroy(gameObject, Delay);
    }
}

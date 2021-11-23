using UnityEngine;
using UnityEngine.Events;

public class PlayerRotater : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public event UnityAction<bool> Rotated;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float x;
        float y;

        x = Input.GetAxis(MouseX);
        y = Input.GetAxis(MouseY);

        if (Input.GetMouseButton(0))
        {
            transform.localEulerAngles += new Vector3(y, x, 0);
            Rotated?.Invoke(true);
        }
        else
            Rotated?.Invoke(false);
    }
}

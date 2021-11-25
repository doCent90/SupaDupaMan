using UnityEngine;
using UnityEngine.Events;

public class PlayerRotater : MonoBehaviour
{
    private GameWin _gameWin;
    private float _axisY;

    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private const float Range = 60f;

    public event UnityAction<bool> Rotated;

    private void OnEnable()
    {
        _gameWin = FindObjectOfType<GameWin>();

        _gameWin.Won += OnWonGame;
    }

    private void OnDisable()
    {
        _gameWin.Won -= OnWonGame;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            float x;
            x = Input.GetAxis(MouseX);
            _axisY += Input.GetAxis(MouseY);
            _axisY = Mathf.Clamp(_axisY, -Range, Range);

            var euler = transform.localEulerAngles;
            euler.x = _axisY;

            transform.localEulerAngles = euler;
            transform.localEulerAngles += new Vector3(0, x, 0);

            Rotated?.Invoke(true);
        }
        else
            Rotated?.Invoke(false);
    }

    private void OnWonGame()
    {
        enabled = false;
    }
}

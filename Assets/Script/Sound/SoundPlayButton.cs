using UnityEngine;

public class SoundPlayButton : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonClick;

    private ButtonsUI _buttons;

    private void OnEnable()
    {
        _buttons = GetComponentInChildren<ButtonsUI>();

        _buttons.Clicked += Play;
    }

    private void OnDisable()
    {
        _buttons.Clicked -= Play;
    }

    private void Play()
    {
        _buttonClick.Play();
    }
}

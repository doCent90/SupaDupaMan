using TMPro;
using System;
using UnityEngine;

public class LevelsViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private ButtonsUI _buttonsUI;

    private void OnEnable()
    {
        if (_buttonsUI == null)
            throw new NullReferenceException(nameof(_buttonsUI));

        Show();
    }

    private void Show()
    {
        _levelText.text = $"Level {_buttonsUI.LevelsLoader.Level}";
    }
}

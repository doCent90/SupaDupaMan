using System;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;

    public StartGame StartGame => _startGame;

    private void OnEnable()
    {
        if(_startGame == null)
            throw new NullReferenceException();
    }
}

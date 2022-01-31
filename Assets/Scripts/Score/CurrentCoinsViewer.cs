using UnityEngine;
using TMPro;
using System;

public class CurrentCoinsViewer : MonoBehaviour
{
    private TMP_Text _coinsTxt;
    private CoinScaler _coinScaler;

    private const int Multiply = 2;
    private const string Coins = "Coins";

    public event Action ScoreChanged;

    private void OnEnable()
    {
        _coinsTxt = GetComponentInChildren<TMP_Text>();
        _coinScaler = GetComponentInChildren<CoinScaler>();

        Show();

        _coinScaler.Rewarded += AddCoin;
    }

    private void OnDisable()
    {
        _coinScaler.Rewarded -= AddCoin;
    }

    private void AddCoin()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);
        totalCoins += Multiply;

        PlayerPrefs.SetInt(Coins, totalCoins);

        ScoreChanged?.Invoke();
        Show();
    }

    private void Show()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);

        _coinsTxt.text = totalCoins.ToString();
    }
}

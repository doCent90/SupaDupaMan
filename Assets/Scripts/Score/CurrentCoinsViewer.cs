using UnityEngine;
using TMPro;

public class CurrentCoinsViewer : MonoBehaviour
{
    private TMP_Text _coinsTxt;
    private CoinScaler _coinScaler;

    private int _currentCoinsCount = 0;

    private const int Multiply = 10;
    private const string Coins = "Coins";

    private void OnEnable()
    {
        _coinScaler = GetComponentInChildren<CoinScaler>();
        _coinsTxt = GetComponentInChildren<TMP_Text>();

        _coinScaler.Rewarded += AddCoin;
    }

    private void OnDisable()
    {
        _coinScaler.Rewarded -= AddCoin;

        SafeScore();
    }

    private void AddCoin()
    {
        _currentCoinsCount += Multiply;
        Show();
    }

    private void Show()
    {
        _coinsTxt.text = _currentCoinsCount.ToString();
    }

    private void SafeScore()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);
        totalCoins += _currentCoinsCount;

        PlayerPrefs.SetInt(Coins, totalCoins);
    }
}

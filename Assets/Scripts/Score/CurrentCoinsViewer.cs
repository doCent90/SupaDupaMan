using UnityEngine;
using TMPro;

public class CurrentCoinsViewer : MonoBehaviour
{
    private CoinScaler _coinScaler;
    private TMP_Text _coinsTxt;

    private int _currentCoins = 0;

    private const int Multiply = 10;
    private const string Coins = "Coins";

    private void OnEnable()
    {
        _coinScaler = GetComponentInChildren<CoinScaler>();
        _coinsTxt = GetComponent<TMP_Text>();

        _coinScaler.Rewarded += AddCoin;
    }

    private void OnDisable()
    {
        _coinScaler.Rewarded -= AddCoin;

        SafeScore();
    }

    private void AddCoin()
    {
        _currentCoins += Multiply;
        Show();
    }

    private void Show()
    {
        _coinsTxt.text = _currentCoins.ToString();
    }

    private void SafeScore()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);
        totalCoins += _currentCoins;

        PlayerPrefs.SetInt(Coins, totalCoins);
    }
}

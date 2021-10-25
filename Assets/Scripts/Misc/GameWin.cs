using UnityEngine;

public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private YouWin _winText;
    private EnemyGigant _enemyGigant;

    private void OnEnable()
    {
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _enemy = _enemyGigant.GetComponent<Enemy>();
        _winText = FindObjectOfType<YouWin>();

        _winText.gameObject.SetActive(false);

        _enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _winText.gameObject.SetActive(true);
    }
}

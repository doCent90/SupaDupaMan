using UnityEngine;

public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private YouWin _winText;
    private EnemyGigant _enemyGigant;
    private PlayerMover _playerMover;

    private void OnEnable()
    {
        _playerMover = FindObjectOfType<PlayerMover>();
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _enemy = _enemyGigant.GetComponent<Enemy>();
        _winText = FindObjectOfType<YouWin>();

        _winText.gameObject.SetActive(false);

        _enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _playerMover.enabled = false;
        _winText.gameObject.SetActive(true);
    }
}

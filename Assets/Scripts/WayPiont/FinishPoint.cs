using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private YouWin _winText;    
    private EnemyGigant _lastEnemy;
    private PlayerMover _playerMover;
    private CameraMover _cameraMover;

    private void OnEnable()
    {
        //_winText = FindObjectOfType<YouWin>();
        _playerMover = FindObjectOfType<PlayerMover>();
        _cameraMover = FindObjectOfType<CameraMover>();
        _lastEnemy = FindObjectOfType<EnemyGigant>();

        //_winText.gameObject.SetActive(false);
        _playerMover.LastPointCompleted += EndLevel;
    }

    private void OnDisable()
    {
        _playerMover.LastPointCompleted -= EndLevel;
    }

    private void EndLevel()
    {
        _lastEnemy.GetComponent<EnemyMover>().enabled = false;
        _cameraMover.enabled = false;
        //_winText.gameObject.SetActive(true);
    }
}

using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(StartRegDollAnimator))]
public class StartRegDollMover : MonoBehaviour
{
    private Exit _exit;
    private Player _player;
    private EnemyGigant _enemyGigant;
    private StartRegDollAnimator _dollAnimator;

    private const float Duration = 3f;

    private void OnEnable()
    {
        _exit = FindObjectOfType<Exit>();
        _player = FindObjectOfType<Player>();
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _dollAnimator = GetComponent<StartRegDollAnimator>();

        _dollAnimator.enabled = true;
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(_player.transform.position.x, _enemyGigant.transform.position.y, _player.transform.position.z);

        var tweenMove = transform.DOMove(new Vector3(_exit.transform.position.x,
            transform.position.y, _exit.transform.position.z), Duration);
        tweenMove.SetEase(Ease.InFlash);
        tweenMove.OnComplete(DisableObject);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

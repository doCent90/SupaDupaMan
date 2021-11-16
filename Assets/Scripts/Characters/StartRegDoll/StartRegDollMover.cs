using UnityEngine;

[RequireComponent(typeof(StartRegDollAnimator))]
public class StartRegDollMover : MonoBehaviour
{
    private Player _player;
    private EnemyGigant _enemyGigant;
    private StartRegDollAnimator _dollAnimator;

    private const float Duration = 3f;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _dollAnimator = GetComponent<StartRegDollAnimator>();

        _dollAnimator.enabled = true;
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

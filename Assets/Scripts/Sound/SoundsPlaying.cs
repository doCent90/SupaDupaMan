using UnityEngine;

public abstract class SoundsPlaying : MonoBehaviour
{
    [SerializeField] protected AudioSource _soundShot;
    [SerializeField] protected AudioSource _soundDeath;
    [SerializeField] protected AudioSource _soundFly;

    protected void Shot()
    {
        _soundShot.Play();
    }

    protected void Death()
    {
        _soundDeath.Play();
    }

    protected void Fly(bool isPlaing)
    {
        if (isPlaing)
            _soundFly.Play();
        else
            _soundFly.Stop();
    }
}

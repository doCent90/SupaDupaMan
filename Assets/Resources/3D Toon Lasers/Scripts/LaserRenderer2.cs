using UnityEngine;

public class LaserRenderer2 : MonoBehaviour
{
    public Color laserColor = new Vector4(1,1,1,1);
    public GameObject HitEffect;
    public GameObject FlashEffect;

    private bool _updateSaver = false;
    private ParticleSystem _laserParticalSysytem;
    private ParticleSystem[] _flashes;
    private ParticleSystem[] _hits;
    private Material _laserMat;
    private int _particleCount;
    private ParticleSystem.Particle[] _particles;
    private Vector3[] _particlesPositions;
    private float _dissovleTimer = 0;
    private bool _isStartDissovle = false;

    private float HitOffset = 0.1f;
    private const float laserScale = 1;
    private const float MaxLength = 80f;
    private const string StartPoint = "_StartPoint";
    private const string Distance = "_Distance";
    private const string EndPoint = "_EndPoint";
    private const string Dissolve = "_Dissolve";
    private const string Scale = "_Scale";

    public void DisablePrepare()
    {
        _dissovleTimer = 0;
        _isStartDissovle = true;
        _updateSaver = true;
        transform.parent = null;

        if (_flashes != null && _hits != null)
        {
            foreach (var AllHits in _hits)
            {
                if (AllHits.isPlaying) AllHits.Stop();
            }
            foreach (var AllFlashes in _flashes)
            {
                if (AllFlashes.isPlaying) AllFlashes.Stop();
            }
        }
    }

    private void Start()
    {
        _laserParticalSysytem = GetComponent<ParticleSystem>();
        _laserMat = GetComponent<ParticleSystemRenderer>().material;
        _flashes = FlashEffect.GetComponentsInChildren<ParticleSystem>();
        _hits = HitEffect.GetComponentsInChildren<ParticleSystem>();
        _laserMat.SetFloat(Scale, laserScale);
    }

    private void Update()
    {
        if (_laserParticalSysytem != null && _updateSaver == false)
        {
            _laserMat.SetVector(StartPoint, transform.position);

            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, MaxLength))
            {
                _particleCount = Mathf.RoundToInt(rayHit.distance / (2 * laserScale));
                if (_particleCount < rayHit.distance / (2 * laserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, rayHit.distance);
                _laserMat.SetVector(EndPoint, rayHit.point);
                if (_hits != null)
                {
                    HitEffect.transform.position = rayHit.point + rayHit.normal * HitOffset;
                    HitEffect.transform.LookAt(rayHit.point);
                    foreach (var hit in _hits)
                    {
                        if (!hit.isPlaying) hit.Play();
                    }
                    foreach (var flash in _flashes)
                    {
                        if (!flash.isPlaying) flash.Play();
                    }
                }
            }
            else
            {
                var endPosition = transform.position + transform.forward * MaxLength;
                var distance = Vector3.Distance(endPosition, transform.position);
                _particleCount = Mathf.RoundToInt(distance / (2 * laserScale));
                if (_particleCount < distance / (2 * laserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, distance);
                _laserMat.SetVector(EndPoint, endPosition);
                if (_hits != null)
                {
                    HitEffect.transform.position = endPosition;
                    foreach (var AllPs in _hits)
                    {
                        if (AllPs.isPlaying) AllPs.Stop();
                    }
                }
            }          
        }

        if (_isStartDissovle)
        {
            _dissovleTimer += Time.deltaTime;
            _laserMat.SetFloat(Dissolve, _dissovleTimer * 5);
        }
    }

    private void AddParticles()
    {
        _particles = new ParticleSystem.Particle[_particleCount];

        for (int i = 0; i < _particleCount; i++)
        {
            _particlesPositions[i] = Vector3.zero + new Vector3(0f, 0f, i * 2 * laserScale);
            _particles[i].position = _particlesPositions[i];
            _particles[i].startSize3D = new Vector3(0.001f, 0.001f, 2 * laserScale);
            _particles[i].startColor = laserColor;
        }
        _laserParticalSysytem.SetParticles(_particles, _particles.Length);
    }
}
 
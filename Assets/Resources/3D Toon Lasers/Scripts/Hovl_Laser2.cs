using UnityEngine;

public class Hovl_Laser2 : MonoBehaviour
{
    public float laserScale = 1;
    public Color laserColor = new Vector4(1,1,1,1);
    public GameObject HitEffect;
    public GameObject FlashEffect;
    public float HitOffset = 0;
    public float MaxLength;

    private bool _updateSaver = false;
    private ParticleSystem _laserParticalSysytem;
    private ParticleSystem[] _flash;
    private ParticleSystem[] _hit;
    private Material _laserMat;
    private int _particleCount;
    private ParticleSystem.Particle[] _particles;
    private Vector3[] _particlesPositions;
    private float _dissovleTimer = 0;
    private bool _isStartDissovle = false;

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

        if (_flash != null && _hit != null)
        {
            foreach (var AllHits in _hit)
            {
                if (AllHits.isPlaying) AllHits.Stop();
            }
            foreach (var AllFlashes in _flash)
            {
                if (AllFlashes.isPlaying) AllFlashes.Stop();
            }
        }
    }

    private void Start()
    {
        _laserParticalSysytem = GetComponent<ParticleSystem>();
        _laserMat = GetComponent<ParticleSystemRenderer>().material;
        _flash = FlashEffect.GetComponentsInChildren<ParticleSystem>();
        _hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
        _laserMat.SetFloat(Scale, laserScale);
    }

    private void Update()
    {
        if (_laserParticalSysytem != null && _updateSaver == false)
        {
            _laserMat.SetVector(StartPoint, transform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                _particleCount = Mathf.RoundToInt(hit.distance / (2 * laserScale));
                if (_particleCount < hit.distance / (2 * laserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, hit.distance);
                _laserMat.SetVector(EndPoint, hit.point);
                if (_hit != null)
                {
                    HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                    HitEffect.transform.LookAt(hit.point);
                    foreach (var AllHits in _hit)
                    {
                        if (!AllHits.isPlaying) AllHits.Play();
                    }
                    foreach (var AllFlashes in _flash)
                    {
                        if (!AllFlashes.isPlaying) AllFlashes.Play();
                    }
                }
            }
            else
            {
                var EndPos = transform.position + transform.forward * MaxLength;
                var distance = Vector3.Distance(EndPos, transform.position);
                _particleCount = Mathf.RoundToInt(distance / (2 * laserScale));
                if (_particleCount < distance / (2 * laserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, distance);
                _laserMat.SetVector(EndPoint, EndPos);
                if (_hit != null)
                {
                    HitEffect.transform.position = EndPos;
                    foreach (var AllPs in _hit)
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
 
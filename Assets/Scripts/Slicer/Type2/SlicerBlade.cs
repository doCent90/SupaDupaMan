using Assets.Scripts;
using UnityEngine;

public class SlicerBlade : MonoBehaviour
{
    //Slicer Plugin download by Git

    [SerializeField]
    [Tooltip("The blade object")]
    private GameObject _blade;
     
    [SerializeField]
    [Tooltip("The empty game object located at the tip of the blade")]
    private GameObject _tip;

    [SerializeField]
    [Tooltip("The empty game object located at the base of the blade")]
    private GameObject _base;

    [SerializeField]
    [Tooltip("The amount of force applied to each side of a slice")]
    private float _forceAppliedToCut = 5f;

    private Vector3 _triggerExitTipPosition;
    private Vector3 _triggerEnterTipPosition;
    private Vector3 _triggerEnterBasePosition;

    private void OnEnable()
    {
        Material bladeMaterial = Instantiate(_blade.GetComponent<MeshRenderer>().sharedMaterial);
        _blade.GetComponent<MeshRenderer>().sharedMaterial = bladeMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerEnterTipPosition = _tip.transform.position;
        _triggerEnterBasePosition = _base.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerExitTipPosition = _tip.transform.position;

        Vector3 side1 = _triggerExitTipPosition - _triggerEnterTipPosition;
        Vector3 side2 = _triggerExitTipPosition - _triggerEnterBasePosition;

        Vector3 normal = Vector3.Cross(side1, side2).normalized;

        Vector3 transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        Vector3 transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(_triggerEnterTipPosition);

        Plane plane = new Plane();

        plane.SetNormalAndPosition(
                transformedNormal,
                transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        if (direction < 0)
        {
            plane = plane.flipped;
        }

        GameObject[] slices = Slicer.Slice(plane, other.gameObject);
        Destroy(other.gameObject);

        Rigidbody rigidbody = slices[1].GetComponent<Rigidbody>();
        Vector3 newNormal = transformedNormal + Vector3.up * _forceAppliedToCut;
        rigidbody.AddForce(newNormal, ForceMode.Impulse);
    }
}
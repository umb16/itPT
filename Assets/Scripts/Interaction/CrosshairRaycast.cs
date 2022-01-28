using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairRaycast : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance = 2;
    [SerializeField] private bool _debug;

    public Vector3 GlobalDir => _centerTransform.forward;
    public GameObject HitObject => _hit.transform?.gameObject;
    public InteractiveObject HitInteractiveObject => _hit.transform?.GetComponent<InteractiveObject>();

    private RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_centerTransform.position, _centerTransform.forward, out _hit, _maxDistance, _layerMask))
        {
            if (_debug)
            {
                Debug.Log(_hit.transform.name);
            }
        }

        if (_debug)
        {
            Debug.DrawRay(_centerTransform.position, _centerTransform.forward*_maxDistance, Color.yellow);
        }
    }
}

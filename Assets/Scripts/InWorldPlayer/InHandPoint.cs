using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHandPoint : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Collider _collider;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _pointTo;


    void Awake()
    {

        var startPos = transform.localPosition;
        var startRotation = transform.localRotation;
        UniTaskAsyncEnumerable.EveryValueChanged(_cameraHolder, x => x.localPosition.y).Subscribe(x =>
        {
            _pointTo = Vector3.Lerp(startPos, startPos + Vector3.up * x * 2.5f, .5f);
        }, cancellationToken: this.GetCancellationTokenOnDestroy());

        UniTaskAsyncEnumerable.EveryValueChanged(_cameraPivot, x => x.localRotation).Subscribe(x =>
        {
            transform.localRotation = Quaternion.Lerp(x, startRotation, .5f);
        }, cancellationToken: this.GetCancellationTokenOnDestroy());
    }

    void Update()
    {
        Vector3 newPos = transform.parent.TransformPoint(_pointTo);
        Vector3 moveDir = newPos - transform.position;
        Debug.DrawLine(transform.position, newPos, Color.cyan);

        Debug.DrawRay(transform.position, moveDir, Color.red);
        transform.position += moveDir;
    }
}

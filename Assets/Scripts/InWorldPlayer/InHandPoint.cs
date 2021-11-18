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

    [HideInInspector] public InteractiveObject InteractiveObject;

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
            //Quaternion.to
            transform.localRotation = Quaternion.Lerp(x, startRotation, .5f);
        }, cancellationToken: this.GetCancellationTokenOnDestroy());
    }

    private Vector3 ResolveCollisions(Vector3 velocity)
    {
        Collider[] colliders = new Collider[10];
        // Get nearby colliders
        Physics.OverlapSphereNonAlloc(transform.position + velocity, 1, colliders, _layerMask);

        var totalDisplacement = Vector3.zero;
        var checkedColliderIndices = new HashSet<int>();

        // If the player is intersecting with that environment collider, separate them
        for (var i = 0; i < colliders.Length; i++)
        {
            // Two player colliders shouldn't resolve collision with the same environment collider
            if (checkedColliderIndices.Contains(i))
            {
                continue;
            }

            var envColl = colliders[i];

            // Skip empty slots
            if (envColl == null)
            {
                continue;
            }

            if (Physics.ComputePenetration(
                _collider, transform.position, transform.rotation,
                envColl, envColl.transform.position, envColl.transform.rotation,
                out Vector3 collisionNormal, out float collisionDistance))
            {
                // Ignore very small penetrations
                // Required for standing still on slopes
                // ... still far from perfect though
                if (collisionDistance < 0.015)
                {
                    continue;
                }
                checkedColliderIndices.Add(i);
                // Get outta that collider!
                totalDisplacement += collisionNormal * collisionDistance;

                // Crop down the velocity component which is in the direction of penetration
                //velocity -= Vector3.Project(velocity, collisionNormal);
            }
        }
        if (checkedColliderIndices.Count > 0)
            return totalDisplacement;
        else
        {
            var dir = velocity;
            for (var i = 0; i < colliders.Length; i++)
            {
                // Two player colliders shouldn't resolve collision with the same environment collider
                if (checkedColliderIndices.Contains(i))
                {
                    continue;
                }

                var envColl = colliders[i];

                // Skip empty slots
                if (envColl == null)
                {
                    continue;
                }

                if (Physics.ComputePenetration(
                    _collider, transform.position + dir, transform.rotation,
                    envColl, envColl.transform.position, envColl.transform.rotation,
                    out Vector3 collisionNormal, out float collisionDistance))
                {
                    // Ignore very small penetrations
                    // Required for standing still on slopes
                    // ... still far from perfect though
                    if (collisionDistance < 0.015)
                    {
                        continue;
                    }
                    checkedColliderIndices.Add(i);
                    // Get outta that collider!
                    //totalDisplacement += collisionNormal * collisionDistance;

                    // Crop down the velocity component which is in the direction of penetration
                    velocity -= Vector3.Project(velocity, collisionNormal);
                }
            }
            if (checkedColliderIndices.Count > 0)
                return velocity;
            else
                return velocity;
        }
    }
    /* private bool ResolveCollisions(out Vector3 velocity)
     {
         Collider[] colliders = new Collider[10];
         // Get nearby colliders
         Physics.OverlapSphereNonAlloc(transform.position, 1, colliders, _layerMask);

         var totalDisplacement = Vector3.zero;
         var checkedColliderIndices = new HashSet<int>();

         // If the player is intersecting with that environment collider, separate them
         for (var i = 0; i < colliders.Length; i++)
         {
             // Two player colliders shouldn't resolve collision with the same environment collider
             if (checkedColliderIndices.Contains(i))
             {
                 continue;
             }

             var envColl = colliders[i];

             // Skip empty slots
             if (envColl == null)
             {
                 continue;
             }

             if (Physics.ComputePenetration(
                 _collider, transform.position, transform.rotation,
                 envColl, envColl.transform.position, envColl.transform.rotation,
                 out Vector3 collisionNormal, out float collisionDistance))
             {
                 checkedColliderIndices.Add(i);
                 // Get outta that collider!
                 totalDisplacement += collisionNormal * collisionDistance;

                 // Crop down the velocity component which is in the direction of penetration
                // velocity -= Vector3.Project(velocity, collisionNormal);
             }
         }
         velocity = totalDisplacement;
         return checkedColliderIndices.Count > 0;
     }*/
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.parent.TransformPoint(_pointTo);
        Vector3 moveDir = newPos - transform.position;
        //Debug.Log("moveDir " + moveDir);
        Debug.DrawLine(transform.position, newPos, Color.cyan);

        moveDir = ResolveCollisions(moveDir);
        Debug.DrawRay(transform.position, moveDir, Color.red);
        //transform.position = Vector3.Lerp(transform.position, moveDir + transform.position, Time.deltaTime * 10);
        transform.position += moveDir;
    }
}

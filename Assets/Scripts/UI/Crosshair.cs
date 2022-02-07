using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Crosshair : MonoBehaviour
{
    private CrosshairRaycast _crosshairRaycast;

    [Inject]
    private void Construct(CrosshairRaycast crosshairRaycast)
    {
        _crosshairRaycast = crosshairRaycast;
    }

    private void Update()
    {
        if (_crosshairRaycast.HitInteractiveObject != null)
        {
            transform.Rotate(0, 0, Time.deltaTime * 40);
        }
        else
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, 45), .5f);
        }
    }
}

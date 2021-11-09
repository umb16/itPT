using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodTestEvent : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _emitter;
    [EditorButton]
    public void PlaySound()
    {
        _emitter.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour, IAction
{
    public LocKeys Name => LocKeys.Take;

    public bool CheckValidity() => true;

    public void Execute()
    {
        Debug.Log("Take action execute");
    }
}

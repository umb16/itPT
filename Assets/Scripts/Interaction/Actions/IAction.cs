using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction 
{
    LocKeys Name { get; }
    public void Execute();
    public bool CheckValidity();
}

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpdateAssets : MonoBehaviour
{
    [SerializeField] private bool _updateOn;
    private double time;
    [EditorButton]
    void UpdateOk()
    {
        if (!_updateOn)
        {
            EditorApplication.update += UpdateE;
        }
        else
        {
            EditorApplication.update -= UpdateE;
        }
        _updateOn = !_updateOn;
    }

    private void UpdateE()
    {
        if (EditorApplication.timeSinceStartup > time)
        {
            AssetDatabase.Refresh();
            Debug.Log("xxx");
            time = EditorApplication.timeSinceStartup + 2;
        }
    }
}
#endif

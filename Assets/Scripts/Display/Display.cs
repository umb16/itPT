using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    private int _sizeX;
    private int _sizeY;
    public void DestroyCells()
    {
        var transforms = GetComponentsInChildren<Transform>();
        for (int i = 1; i < transforms.Length; i++)
        {
            Destroy(transforms[i].gameObject);
        }
    }
    public void Create(int xsize, int ysize)
    {
        _sizeX = xsize;
        _sizeY = ysize;
        DestroyCells();
    }
}

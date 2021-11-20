using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _startPos + new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time * 1.1f), 0);
    }
}

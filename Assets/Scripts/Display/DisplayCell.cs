using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCell : MonoBehaviour
{
    [SerializeField] private Image _image;
    public bool Empty { get; internal set; } = true;
    public void SetImage(Sprite sprite, bool soft)
    {
        Empty = sprite.name == "space" || soft;

        _image.sprite = sprite;
    }
}

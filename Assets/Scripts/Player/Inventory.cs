using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _maxSlots = 4;
    private int _selectedSlot;
    private List<InteractiveObject> _interactiveObjects = new List<InteractiveObject>();
    private InteractiveObject _inHandObj;

    public InteractiveObject CurrentObject => _interactiveObjects.Count > 0 ? _interactiveObjects[_selectedSlot] : null;
    public bool IsFull => _interactiveObjects.Count == _maxSlots;

    public void Add(InteractiveObject obj)
    {
        if (!_interactiveObjects.Contains(obj) && !IsFull)
        {
            _interactiveObjects.Add(obj);
        }
    }
    public void Remove(InteractiveObject obj)
    {
        if (_inHandObj == obj)
        {
            
        }
    }
    public void SelectNext()
    {
        _selectedSlot++;
        if (_selectedSlot >= _maxSlots)
            _selectedSlot = 0;
    }
    public void SelectPrev()
    {
        _selectedSlot--;
        if (_selectedSlot < 0)
            _selectedSlot = _maxSlots - 1;        
    }
}

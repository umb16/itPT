using System;

public class Stat
{
    public event Action ValueChanged;

    private float _value;
    private float _maxValue = 100;
    private char _icon = ' ';

    public float Value
    {
        get => _value;
        set
        {
            if(value > _maxValue)
                value = _maxValue;
            if (value != _value)
            {
                ValueChanged.Invoke();
                _value = value;
            }
        }
    }

    public float MaxValue => _maxValue;
    public char Icon => _icon;

    public Stat(float value, float maxValue, char icon)
    {
        _value = value;
        _maxValue = maxValue;
        _icon = icon;
    }

    public void Update()
    {
        ValueChanged.Invoke();
    }
}

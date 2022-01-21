using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatsVisualizer
{
    private StatContainer _statContainer;
    private DisplayWriter _displayWriter;

    [Inject]
    private void Construct(StatContainer statContainer, DisplayWriter displayWriter)
    {
        _statContainer = statContainer;
        _displayWriter = displayWriter;

        _statContainer.Cheerfulness.ValueChanged += () =>
        {
            _displayWriter.SetCursorPos(0, 2);
            PrintStat(_statContainer.Cheerfulness);
        };
        _statContainer.Water.ValueChanged += () =>
        {
            _displayWriter.SetCursorPos(0, 3);
            PrintStat(_statContainer.Water);
        };
        _statContainer.Energy.ValueChanged += () =>
        {
            _displayWriter.SetCursorPos(0, 4);
            PrintStat(_statContainer.Energy);
        };
        _statContainer.Health.ValueChanged += () =>
        {
            _displayWriter.SetCursorPos(0, 5);
            PrintStat(_statContainer.Health);
        };
        _statContainer.Cold.ValueChanged += () =>
        {
            _displayWriter.SetCursorPos(0, 6);
            if (_statContainer.Cold.Value < 100)
                PrintStat(_statContainer.Cold);
            else
                ClearLine(9);
        };
        _statContainer.Void.ValueChanged += () =>
        {
            if (_statContainer.Cold.Value < 100)
                _displayWriter.SetCursorPos(0, 7);
            else
                _displayWriter.SetCursorPos(0, 6);
            if (_statContainer.Void.Value < 100)
                PrintStat(_statContainer.Void);
            else
                ClearLine(9);
        };

        _statContainer.Update();

    }



    private void PrintStat(Stat stat)
    {
        _displayWriter.Print(" " + stat.Icon);
        _displayWriter.Print(WarpedbConvertor.GenerateBar(5, stat.MaxValue, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, stat.Value));
        _displayWriter.Print("ð");
    }

    public void ClearLine(int lenght)
    {
        _displayWriter.Print(new string(' ', lenght));
    }
}

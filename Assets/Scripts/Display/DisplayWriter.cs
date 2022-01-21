using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWriter
{
    [SerializeField] private Display _display;
    private Vector2Int _cursorPos = Vector2Int.zero;

    public DisplayWriter(Display display)
    {
        _display = display;
    }

    public void NewLine()
    {
        _cursorPos.x = 0;
        _cursorPos.y++;
    }
    public void Reset()
    {
        _cursorPos.x = 0;
        _cursorPos.y = 0;
    }
    public void Home()
    {
        _cursorPos.x = 0;
    }

    public void SetCursorPos(int x, int y)
    {
        if (x < 0)
            x = _display.SizeX + x;
        if (y < 0)
            y = _display.SizeY + y;
        _cursorPos.x = x;
        _cursorPos.y = y;
    }
    public void SetCursorPos(Vector2Int pos)
    {
        _cursorPos = pos;
    }

    public void InvertPack(bool value)
    {
        _display.InvertPack(value);
    }

    public void Print(string text, params int[] numbers)
    {
        Print(text, false, numbers);
    }

    public void Print(string text, bool soft, params int[] numbers)
    {
        int currentNumberIndex = 0;
        string[] strings = text.Split("%n");
        List<string> keys = new List<string>();
        for (int i = 0; i < strings.Length; i++)
        {
            keys.AddRange(WarpedbConvertor.ConvertStringToKeys(strings[i]));
            if (numbers.Length > currentNumberIndex)
            {
                keys.AddRange(WarpedbConvertor.ConvertNumberToKeys(numbers[currentNumberIndex]));
                currentNumberIndex++;
            }
        }
        foreach (var key in keys)
        {
            _display.SetCell(_cursorPos.x, _cursorPos.y, key, soft).Forget();
            _cursorPos.x++;
        }
    }

    public void PrintN(string text, params int[] numbers)
    {
        Print(text, numbers);
        NewLine();
    }
}

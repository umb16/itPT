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
        bool showCursor = false;
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
        _cursorPos.x = x;
        _cursorPos.y = y;
    }

    public void Print(string text, params int[] numbers)
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
            _display.SetCell(_cursorPos.x, _cursorPos.y, key);
            _cursorPos.x++;
        }
    }

    private void DisplayCursor(bool value)
    {
        _display.SetCell(_cursorPos.x, _cursorPos.y, value ? "cursor" : "space");
    }

    public void PrintN(string text, params int[] numbers)
    {
        Print(text, numbers);
        NewLine();
    }
}

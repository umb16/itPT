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
        int count = 0;
        UniTaskAsyncEnumerable
            .Timer(new System.TimeSpan(), new System.TimeSpan(0, 0, 0, 0, 50))
            .ForEachAwaitAsync(async _ =>
            {
                await UniTask.NextFrame();
                showCursor = !showCursor;
                //DisplayCursor(showCursor);
                count+=100;
                Reset();
                _display.Clear();
                NewLine();
                PrintN(" новогодн€€ распродажа");
                Print(" M");
                Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count) % 9999));
                PrintN("р");
                Print(" D");
                Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count) % 9999));
                PrintN("р");
                Print(" E");
                Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count) % 9999));
                PrintN("р");
                Print(" h");
                Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count) % 9999));
                PrintN("р");
                Print(" S");
                Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count) % 9999));
                PrintN("р");
                Print((new char[]{',', 'i', '|', 'l', 'b', 'B', 'b', 'l','|', 'i'})[count%10].ToString());
                PrintN((new char[]{'|', 'l', 'b', 'B', 'b', 'l','|'})[count%7].ToString());
                //Print(WarpedbConvertor.GenerateBar(5, 9999, new char[] { ',', '.', 'i', '|', 'l', 'b', 'B' }, (count)%9999));
                //PrintN("р");
            });
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

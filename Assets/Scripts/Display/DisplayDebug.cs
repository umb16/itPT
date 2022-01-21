using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Threading;
using UnityEngine;

public class DisplayDebug
{
    private Display _display;
    private DisplayWriter _displayWriter;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private Vector2Int _displayCenter;

    public DisplayDebug(Display display, DisplayWriter displayWriter, CancellationTokenSource cancellationTokenSource)
    {
        _display = display;
        _displayWriter = displayWriter;
        _cancellationTokenSource = cancellationTokenSource;
        Update().Forget();
    }
    private async UniTask Update()
    {
        await UniTask.WaitUntil(() => _display.Ready);
        await foreach (var _ in UniTaskAsyncEnumerable.TimerFrame(0,50).WithCancellation(_cancellationTokenSource.Token))
        {
            _displayCenter = new Vector2Int(_display.SizeX / 2, _display.SizeY / 2);
            Debug.Log("_displayCenter " + _displayCenter);
            Debug.Log("_displaySize " + _display.SizeX + " " + _display.SizeY);
            _displayWriter.SetCursorPos(0, _displayCenter.y);
            _displayWriter.Print(new string('.', _display.SizeX), true);
            _displayWriter.SetCursorPos(_displayCenter);
            _displayWriter.Print("B", true);
        }
    }
}

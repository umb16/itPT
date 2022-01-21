using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DisplayDataTime
{
    private DisplayWriter _displayWriter;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public DisplayDataTime(DisplayWriter displayWriter, CancellationTokenSource cancellationTokenSource)
    {
        _displayWriter = displayWriter;
        _cancellationTokenSource = cancellationTokenSource;
        Update().Forget();
    }
    private async UniTask Update()
    {
        await foreach (var _ in UniTaskAsyncEnumerable.Timer(new TimeSpan(),TimeSpan.FromSeconds(1)).WithCancellation(_cancellationTokenSource.Token))
        {
            _displayWriter.SetCursorPos(-2, 0);
            _displayWriter.Print("%n", (int)(Time.time%99));
        }
    }
}

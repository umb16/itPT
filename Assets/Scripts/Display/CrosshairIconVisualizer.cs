using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CrosshairIconVisualizer
{
    private CrosshairRaycast _crosshairRaycast;
    private Display _display;
    private DisplayWriter _displayWriter;
    private Vector2Int _displayCenter;

    public CrosshairIconVisualizer(CrosshairRaycast crosshairRaycast, Display display, DisplayWriter displayWriter)
    {
        _crosshairRaycast = crosshairRaycast;
        _display = display;
        _displayWriter = displayWriter;

        CheckRay().Forget();
    }

    private async UniTask CheckRay()
    {
        await UniTask.WaitUntil(() => _display.Ready);
        await foreach (var _ in UniTaskAsyncEnumerable.EveryValueChanged(_crosshairRaycast, x => x.HitInteractiveObject))
        {
            ObjectMobility mobility = _crosshairRaycast.HitInteractiveObject?.Mobility ?? ObjectMobility.Static;
            _displayCenter = new Vector2Int(_display.SizeX / 2, _display.SizeY / 2);
            Debug.Log("mobility " + mobility);
            _displayWriter.SetCursorPos(_displayCenter);
            switch (mobility)
            {
                case ObjectMobility.Portable:
                case ObjectMobility.Pocket:
                    _displayWriter.Print("H");
                    break;
                /*case ObjectMobility.Static:
                    break;
                case ObjectMobility.Movable:
                    break;
                case ObjectMobility.Pocket:
                    break;*/
                default:
                    _displayWriter.Print("  ");
                    break;
            }
        }
    }
}

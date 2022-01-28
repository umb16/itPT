using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    private CrosshairRaycast _crosshairRaycast;
    private Display _display;
    private InHandPoint _inHandPoint;
    private bool isEmpty => _interactiveObject == null;
    private InteractiveObject _interactiveObject;
    public Hand(CrosshairRaycast crosshairRaycast, Display display, DisplayWriter displayWriter, InHandPoint inHandPoint)
    {
        _crosshairRaycast = crosshairRaycast;
        _display = display;
        _inHandPoint = inHandPoint;
        Debug.Log("Hand init");
        TakeUpdate().Forget();
        DropUpdate().Forget();
    }
    private async UniTask TakeUpdate()
    {
        //await UniTask.WaitUntil(() => _display.Ready);
        await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.E))/*.EveryValueChanged(_crosshairRaycast, x => x.HitInteractiveObject)*/)
        {
            if (_crosshairRaycast.HitInteractiveObject != null)
            {
                var hitObject = _crosshairRaycast.HitInteractiveObject;
                Debug.Log("Hand <-" + hitObject.name);
                if (isEmpty)
                {
                    hitObject.transform.SetParent(_inHandPoint.transform);
                    hitObject.transform.localPosition = Vector3.zero;
                    hitObject.transform.localRotation = Quaternion.identity;
                    hitObject.ToHandMode(_inHandPoint.gameObject.layer);
                    _interactiveObject = hitObject;
                }
            }
        }
    }
    private async UniTask DropUpdate()
    {
        //await UniTask.WaitUntil(() => _display.Ready);
        await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Q)))
        {
            if (!isEmpty)
            {
                Debug.Log("Hand ->" + _interactiveObject.name);
                _interactiveObject.transform.SetParent(null);
                _interactiveObject.ToFreeMode(_crosshairRaycast.GlobalDir * 100);
                _interactiveObject = null;
            }
        }
    }
}

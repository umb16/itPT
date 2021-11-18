using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;

public class CrosshairIcon : MonoBehaviour
{
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private Image _image;
    //private Image _image;
    private CrosshairRaycast _crosshairRaycast;

    [Inject]
    private void Construct(CrosshairRaycast crosshairRaycast)
    {
        _crosshairRaycast = crosshairRaycast;
        UniTaskAsyncEnumerable.EveryValueChanged(crosshairRaycast, x => x.HitInteractiveObject).Subscribe(x =>
        {
            //await UniTask.WaitForEndOfFrame();
            ObjectMobility mobility = x?.Mobility ?? ObjectMobility.Static;
            Debug.Log("mobility " + mobility);
            switch (mobility)
            {
                case ObjectMobility.Portable:
                case ObjectMobility.Pocket:
                    _image.sprite = _icons[1];
                    break;
                /*case ObjectMobility.Static:
                    break;
                case ObjectMobility.Movable:
                    break;
                case ObjectMobility.Pocket:
                    break;*/
                default:
                    _image.sprite = _icons[0];
                    break;
            }
        });
    }
    // Start is called before the first frame update
    void Awake()
    {
        //_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("mobility " + (_crosshairRaycast.HitInteractiveObject?.Mobility ?? ObjectMobility.Static));
    }
}

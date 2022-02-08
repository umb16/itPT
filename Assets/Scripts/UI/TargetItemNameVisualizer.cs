using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
namespace Iptf.RadialMenuVisual
{
    public class TargetItemNameVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private MenuElementState _state = MenuElementState.Disabled;

        private float _startTime = 0;
        private float SinceTime => Time.time - _startTime;
        private float _startAlpha = 1;
        private float _startScale = 1;
        private string _newText = "";
        [Inject]
        private void Construct(CrosshairRaycast crosshairRaycast)
        {
            UniTaskAsyncEnumerable.EveryValueChanged(crosshairRaycast, x => x.HitInteractiveObject).Subscribe(x =>
            {
                _startTime = Time.time;
                if (x != null)
                {
                    if (_state == MenuElementState.Disabled)
                    {
                        _text.text = x.name;
                        _state = MenuElementState.Showing;
                        _text.color = Color.black;
                        _startScale = 0;
                        _text.gameObject.SetActive(true);
                        _text.transform.localEulerAngles = Vector3.zero;
                    }
                    else
                    {
                        _state = MenuElementState.Changing;
                        _startScale = _text.transform.localScale.x;
                        _startAlpha = _text.color.a;
                        _newText = x.name;
                    }
                }
                else
                {
                    _startScale = _text.transform.localScale.x;
                    _state = MenuElementState.Hiding;
                }
            });
        }
        private void Update()
        {
            float sinceTimeScaled = SinceTime * 4;
            /*if (_state == MenuElementState.Showing)
            {
                _text.color = Color.Lerp(new Color(0, 0, 0, _startAlpha), new Color(0, 0, 0, 1), sinceTimeScaled);
            }*/


            _text.transform.localScale = new Vector3(Mathf.Lerp(_startScale, 1, AnimationFuncs.Smooth(sinceTimeScaled)), 1, 1);

            if (_state != MenuElementState.Hiding)
            {
                _text.transform.localEulerAngles = Vector3.Lerp(_text.transform.localEulerAngles, Vector3.zero,Time.deltaTime);
            }

            if (_state == MenuElementState.Changing)
            {
                _text.color = Color.Lerp(new Color(0, 0, 0, _startAlpha), new Color(0, 0, 0, 0), sinceTimeScaled);
                if (sinceTimeScaled > 1)
                {
                    _text.text = _newText;
                    _text.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), sinceTimeScaled-1);
                }
                if (sinceTimeScaled > 2)
                {
                    _state = MenuElementState.Idle;
                }
            }


            if (_state == MenuElementState.Hiding)
            {
                _text.transform.localEulerAngles = _text.transform.localEulerAngles + Vector3.forward * Time.deltaTime;
                _text.color = Color.Lerp(new Color(0, 0, 0, _startAlpha), new Color(0, 0, 0, 0), sinceTimeScaled);
                if (sinceTimeScaled > 1)
                {
                    _state = MenuElementState.Disabled;
                    _text.gameObject.SetActive(false);
                }
            }
        }
    }
}

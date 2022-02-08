using TMPro;
using UnityEngine;

namespace Iptf.RadialMenuVisual
{
    public class MenuElement
    {
        public MenuElementState State { get; private set; } = MenuElementState.Disabled;
        public int Number { get; private set; }
        public TMP_Text TMPText { get; private set; }
        public string Text { get; private set; }

        public MenuElement(int number, TMP_Text tMPText)
        {
            Number = number;
            TMPText = tMPText;
        }

        private Vector3 _startPosition = Vector2.zero;
        private Quaternion _startRotation = Quaternion.identity;
        private float _startAlpha = 1;
        private float _startScale = 1;

        private float _startTime;
        private int _count;
        private int CountMinus1 => _count - 1;
        private float SinceTime => Time.time - _startTime;

        public void SetText(string text, int count, float startTime)
        {
            _count = count;
            _startTime = startTime;

            if (string.IsNullOrEmpty(text))
            {
                if (State != MenuElementState.Disabled)
                {
                    State = MenuElementState.Hiding;
                    _startAlpha = TMPText.color.a;
                    _startScale = TMPText.transform.localScale.x;
                }
            }
            else
            {
                if (State == MenuElementState.Disabled)
                {
                    _startPosition = Vector3.zero;
                    Text = text;
                    TMPText.text = text;
                    TMPText.gameObject.SetActive(true);
                    State = MenuElementState.Showing;
                    TMPText.transform.localPosition = new Vector3(-40 + Mathf.Abs((Number - (CountMinus1 * .5f))) * 10, -40 * (Number - CountMinus1 * .5f), 0);
                    _startPosition = TMPText.transform.localPosition;
                    TMPText.transform.localEulerAngles = new Vector3(0, 0, (9 - CountMinus1 * .5f) * (Number - CountMinus1 * .5f));
                    _startRotation = TMPText.transform.rotation;
                    TMPText.color = Color.black;
                    _startAlpha = 1;
                    //Debug.Log("  " + (-40 * (Number - CountMinus1 * .5f)));
                }
                else
                if (Text != text || State == MenuElementState.Hiding || State == MenuElementState.Idle)
                {
                    Text = text;
                    State = MenuElementState.Changing;
                    _startPosition = TMPText.transform.localPosition;
                    _startAlpha = TMPText.color.a;
                    _startScale = TMPText.transform.localScale.x;
                    _startRotation = TMPText.transform.rotation;
                }
            }
            //Debug.Log("state " + State);
        }

        private void SetStartPos()
        {

        }

        public void Animate()
        {
            if (State == MenuElementState.Disabled)
                return;
            if (State == MenuElementState.Showing)
            {
                float sinceTimeScaled = SinceTime * (4 + CountMinus1 * .4f);
                TMPText.transform.localPosition =
                    Vector3.Lerp(_startPosition, new Vector3(-40 + Mathf.Abs((Number - (CountMinus1 * .5f))) * 10, -40 * (Number - CountMinus1 * .5f), 0),
                    AnimationFuncs.Smooth(sinceTimeScaled - Number * .5f));

                /*TMPText.transform.localEulerAngles =
                    Vector3.Lerp(new Vector3(0, 0, _startRotation), new Vector3(0, 0, (9 - CountMinus1 * .5f) * (Number - CountMinus1 * .5f)),
                    AnimationFuncs.Smooth(sinceTimeScaled - Number));*/
                TMPText.transform.rotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(0, 0, (9 - CountMinus1 * .5f) * (Number - CountMinus1 * .5f)), sinceTimeScaled);
                TMPText.transform.localScale = new Vector3(AnimationFuncs.Smooth(sinceTimeScaled - Number * .5f), 1, 1);
            }
            if (State == MenuElementState.Changing)
            {
                float sinceTimeScaled = SinceTime * 6;
                TMPText.transform.localScale = new Vector3(Mathf.Lerp(_startScale, 1, sinceTimeScaled + _startScale), 1, 1);
                TMPText.transform.localPosition = 
                    Vector3.Lerp(_startPosition, new Vector3(-40 + Mathf.Abs((Number - (CountMinus1 * .5f))) * 10, -40 * (Number - CountMinus1 * .5f), 0),
                    AnimationFuncs.Smooth(sinceTimeScaled));
                TMPText.transform.rotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(0,0, (9 - CountMinus1 * .5f) * (Number - CountMinus1 * .5f)), sinceTimeScaled);
                /*TMPText.transform.localEulerAngles =
                    Vector3.Lerp(new Vector3(0, 0, _startRotation), new Vector3(0, 0, (9 - CountMinus1 * .5f) * (Number - CountMinus1 * .5f)),
                    AnimationFuncs.Smooth(sinceTimeScaled - Number));*/

                TMPText.color = Color.Lerp(new Color(0, 0, 0, _startAlpha), new Color(0, 0, 0, 0), sinceTimeScaled);
                if (sinceTimeScaled > 1)
                {
                    TMPText.text = Text;
                    TMPText.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), sinceTimeScaled - 1);
                }
                if (sinceTimeScaled > 2)
                {
                    State = MenuElementState.Idle;
                }
            }
            if (State == MenuElementState.Hiding)
            {
                float sinceTimeScaled = SinceTime * 4;
                TMPText.color = Color.Lerp(new Color(0, 0, 0, _startAlpha), new Color(0, 0, 0, 0), sinceTimeScaled);
                TMPText.transform.localScale = new Vector3(Mathf.Lerp(_startScale, 1, sinceTimeScaled + _startScale), 1, 1);
                TMPText.transform.localEulerAngles = TMPText.transform.localEulerAngles + Vector3.forward * Time.deltaTime;
                if (sinceTimeScaled > 1)
                {
                    State = MenuElementState.Disabled;
                    TMPText.gameObject.SetActive(false);
                }
            }
        }
    }
}

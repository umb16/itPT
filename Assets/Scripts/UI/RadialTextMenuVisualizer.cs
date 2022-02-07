using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RadialTextMenuVisualizer : MonoBehaviour
{
    [SerializeField] private string[] Texts;
    [SerializeField] private string[] _texts;
    [SerializeField] private TMP_Text[] _tmpTexts;

    private Dictionary<string, TMP_Text> _textsDictionary = new Dictionary<string, TMP_Text>();
    private float _startAnimTime = 0;
    private int _activeCount = 0;

    private CrosshairRaycast _crosshairRaycast;

    [Inject]
    private void Construct(CrosshairRaycast crosshairRaycast)
    {
        _crosshairRaycast = crosshairRaycast;
        UniTaskAsyncEnumerable.EveryValueChanged(_crosshairRaycast, x => x.HitInteractiveObject).Subscribe(x =>
        {
            if (x != null)
            {
                SetTexts();
            }
            else
            {
                SetTexts(new string[0]);
            }
        });
    }

    public void SetTexts(string[] texts)
    {
        _startAnimTime = Time.time;
        _activeCount = 0;
        _texts = texts;
        for (int i = 0; i < _tmpTexts.Length; i++)
        {
            TMP_Text text = _tmpTexts[i];
            if (_texts.Length > i)
            {
                if (!text.gameObject.activeSelf)
                {
                    text.text = _texts[i];
                    text.gameObject.SetActive(true);
                    text.transform.localPosition = Vector3.zero;
                    text.transform.localEulerAngles = Vector3.zero;
                    text.transform.localScale = new Vector3(0, 1, 1);
                    text.color = Color.black;
                }
                else
                {
                    _activeCount++;
                }
            }
        }
    }

    private void SetTexts()
    {
        SetTexts((string[])Texts.Clone());
    }



    private void Update()
    {

        float timeSince = Time.time - _startAnimTime;
        float timeSinceScaled = timeSince * _texts.Length * 2;
        int count = Mathf.Max(_activeCount, Mathf.Min(_texts.Length, Mathf.FloorToInt(timeSinceScaled + 1)));
        float speed = 5f;
        int countMinus1 = count - 1;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 360 - countMinus1 * 3), Time.deltaTime * speed);
        for (int i = 0; i < count; i++)
        {
            var text = _tmpTexts[i];
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, new Vector3(-40 + i * 10 - countMinus1 * 5, -40 * (i - countMinus1 * .5f), 0), Time.deltaTime * speed);
            if (i >= _activeCount || _tmpTexts[i].transform.localScale.x < 1)
            {
                text.transform.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, (9 - countMinus1 * .5f) * i), timeSinceScaled - i);
                _tmpTexts[i].transform.localScale = Vector3.Lerp(_tmpTexts[i].transform.localScale, Vector3.one, timeSinceScaled - i + Time.deltaTime * speed);
            }
        }
        float timeSincex2 = timeSince * 3;
        for (int i = _texts.Length; i < _tmpTexts.Length; i++)
        {
            var text = _tmpTexts[i];
            if (timeSincex2 < 1)
            {
                text.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), timeSincex2);
            }
            if (timeSincex2 > 1)
                text.gameObject.SetActive(false);
        }

        for (int i = 0; i < Mathf.Min(_activeCount, _texts.Length); i++)
        {
            var text = _tmpTexts[i];
            if (_texts[i] != text.text || text.color != Color.black)
            {
                if (timeSincex2 < 1)
                    text.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), timeSincex2);
                if (timeSincex2 >= 1 && timeSincex2 < 4)
                {
                    text.text = _texts[i];
                    text.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, timeSincex2 * .5f - .5f);
                }
                Debug.Log(text.color);
            }
        }
    }
}

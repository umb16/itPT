using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Iptf.RadialMenuVisual
{
    public class RadialTextMenuVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _tmpTexts;
        private MenuElement[] _elements;

        private CrosshairRaycast _crosshairRaycast;

        [Inject]
        private void Construct(CrosshairRaycast crosshairRaycast)
        {
            _elements = new MenuElement[_tmpTexts.Length];
            for (int i = 0; i < _tmpTexts.Length; i++)
            {
                TMP_Text item = _tmpTexts[i];
                _elements[i] = new MenuElement(i, item);
            }
            _crosshairRaycast = crosshairRaycast;
            UniTaskAsyncEnumerable.EveryValueChanged(_crosshairRaycast, x => x.HitInteractiveObject).Subscribe(x =>
            {
                if (x != null)
                {
                    int rand = Random.Range(0, 9);
                    string[] list = new string[rand];
                    for (int i = 0; i < rand; i++)
                    {
                        list[i] = new[] { "[E] поднять", "[E] пнуть", "[E] подобрать ключ", "[E] толкнуть плечём", "[E] использовать" }[Random.Range(0,5)];
                    }
                    SetTexts(list);
                }
                else
                {
                    SetTexts(new string[0]);
                }
            });
        }

        public void SetTexts(string[] texts)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (i < texts.Length)
                {
                    _elements[i].SetText(texts[i], texts.Length, Time.time);
                }
                else
                {
                    _elements[i].SetText(null, texts.Length, Time.time);
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Animate();
            }
        }
    }
}

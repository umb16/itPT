using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Display : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private string[] _spritesPaths;

    private DisplayCell[] _displayCells;
    private CancellationTokenSource _cancellationTokenSource;

    public class SpritesPacks
    {
        private Dictionary<string, Sprite>[] _dictinaries;
        public int _packIndex = 0;

        public SpritesPacks(string[] paths)
        {
            _dictinaries = new Dictionary<string, Sprite>[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                _dictinaries[i] = Resources.LoadAll<Sprite>(paths[i]).ToDictionary((x) => x.name);
            }
        }

        public void SetPackIndex(int index)
        {
            _packIndex = Mathf.Max(0, Mathf.Min(_dictinaries.Length-1, index));
            
        }

        public Sprite GetSprite(string name)
        {
            return _dictinaries[_packIndex][name];
        }
    }

    private SpritesPacks _spritesPacks;
    //private DisplayWriter _displayWriter;
    private int CanvasWidth => (int)_canvasTransform.sizeDelta.x;
    private int CanvasHeight => (int)_canvasTransform.sizeDelta.y;

    public int SizeX => _sizeX;
    public int SizeY => _sizeY;

    public bool Ready { get; internal set; }

    private int _sizeX;
    private int _sizeY;

    [Inject]
    private void Construct(CancellationTokenSource cancellationTokenSource)
    {
        _cancellationTokenSource = cancellationTokenSource;

        _spritesPacks = new SpritesPacks(_spritesPaths);

    }

    private void Start()
    {
        Create(20, 30, 54);
        Ready = true;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }

    public void DestroyCells()
    {
        var transforms = GetComponentsInChildren<Transform>();
        for (int i = 1; i < transforms.Length; i++)
        {
            Destroy(transforms[i].gameObject);
        }
    }

    public void InvertPack(bool value)
    {
        if (value)
            _spritesPacks.SetPackIndex(1);
        else
            _spritesPacks.SetPackIndex(0);
    }

    public void Clear()
    {
        foreach (var cell in _displayCells)
        {
            cell.SetImage(_spritesPacks.GetSprite("space"), true);
        }
    }

    public async UniTaskVoid SetCell(int x, int y, string id, bool soft)
    {
        if (!Ready)
            await UniTask.WaitUntil(() => Ready, cancellationToken: _cancellationTokenSource.Token);
        var currentCell = _displayCells[(x + y * _sizeX) % (_sizeX * _sizeY)];
        if (currentCell.Empty && soft || !soft)
            currentCell.SetImage(_spritesPacks.GetSprite(id), soft);
    }

    public void Create(int ysize, int cellSizex, int cellSizey)
    {
        cellSizex = (int)(cellSizex * 20 / (float)ysize);
        cellSizey = (int)(cellSizey * 20 / (float)ysize);
        _sizeX = Mathf.CeilToInt(CanvasWidth / (20f / ysize * cellSizex));
        _sizeY = ysize;
        DestroyCells();
        _displayCells = new DisplayCell[_sizeX * _sizeY];
        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                var cell = Instantiate(_cellPrefab, _canvasTransform);
                cell.GetComponent<RectTransform>().anchoredPosition = new Vector3(x * cellSizex, -y * cellSizey, 0);
                var displayCell = cell.GetComponent<DisplayCell>();
                displayCell.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSizex, cellSizey);
                displayCell.SetImage(_spritesPacks.GetSprite("space"), true);
                _displayCells[x + y * _sizeX] = displayCell;
                cell.SetActive(true);
            }
        }
    }
}

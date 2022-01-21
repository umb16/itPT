using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Display : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private string _spritesPath;

    private DisplayCell[] _displayCells;
    private Dictionary<string, Sprite> _spritesPack;
    //private DisplayWriter _displayWriter;
    private int CanvasWidth => (int)_canvasTransform.sizeDelta.x;
    private int CanvasHeight => (int)_canvasTransform.sizeDelta.y;

    public int SizeX => _sizeX;
    public int SizeY => _sizeY;

    private int _sizeX;
    private int _sizeY;

    [Inject]
    private void Construct()
    {
        _spritesPack = Resources.LoadAll<Sprite>(_spritesPath).ToDictionary((x) => x.name);
        Create(20, 30, 54);
    }

    private void Start()
    {
       
    }

    public void DestroyCells()
    {
        var transforms = GetComponentsInChildren<Transform>();
        for (int i = 1; i < transforms.Length; i++)
        {
            Destroy(transforms[i].gameObject);
        }
    }

    public void Clear()
    {
        foreach (var cell in _displayCells)
        {
            cell.SetImage(_spritesPack["space"]);
        }
    }

    public void SetCell(int x, int y, string id)
    {
        _displayCells[(x + y * _sizeX)%(_sizeX*_sizeY)].SetImage(_spritesPack[id]);
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
                displayCell.SetImage(_spritesPack["space"]);
                _displayCells[x + y * _sizeX] = displayCell;
                cell.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlidingArea : GridPlacementSystemManager<SlidingArea>
{
    [Header("Variables")]
    private const float _cellSpacing = .5f;
    private const float _itemScale = .5f;
    private bool CanPlace { get => Cursor.Instance.CanPlace(_currentGridItem) && _currentGridItem.CanPlace; }
    private float _maxXPos;
    private Coroutine _slideCoroutine;

    [Header("Components")]
    private GridItem _currentGridItem;
    private List<GridItem> _inventoryItems;

    [Header("Transforms")]
    private Transform _itemParent;

    public UnityEvent WinEvent;

    private void Start()
    {
        _itemParent = transform.GetChild(0);
        InitializeGridItems();
    }

    private void InitializeGridItems()
    {
        _inventoryItems = new List<GridItem>();
        GridItem[] items = FindObjectsOfType<GridItem>();
        for (int i = 0; i < items.Length; i++)
        {
            AddGridItemToInventory(items[i]);
        }

    }

    public void SetCurrentGridItem(GridItem GridItem)
    {
        if (GridItem == _currentGridItem) return;

        if (GridItem == null)
        {
            if (!CanPlace)
            {
                _currentGridItem.ResetPosition();
                _currentGridItem.transform.parent = _itemParent;
                AddGridItemToInventory(_currentGridItem);
            }
            else
            {
                _currentGridItem.transform.position = Cursor.Instance.cursor.position + new Vector3(_currentGridItem.Width / 2f * GridManager.Instance.GlobalCellSize, _currentGridItem.Height / 2f * GridManager.Instance.GlobalCellSize, -(_currentGridItem.Depth / 2f * GridManager.Instance.GlobalCellSize));
                _currentGridItem.transform.parent = null;
                RemoveGridItemFromInventory(_currentGridItem);
            }
        }
        _currentGridItem = GridItem;
    }

    private void Update()
    {
        if (!Cursor.Instance.IsBelow)
        {
            UpdateCurrentGridItemPosition();
        }
        else
        {
            if (_currentGridItem != null) _currentGridItem.ResetPosition();
        }
    }

    public void UpdateSlidingAreaPosition(float dir)
    {
        if (!Cursor.Instance.IsBelow) return;
        if (_currentGridItem != null)
        {
            _currentGridItem.ResetPosition();
            if (_currentGridItem.transform.parent != _itemParent) _currentGridItem.transform.parent = _itemParent;
        }
        if (_slideCoroutine != null) StopCoroutine(_slideCoroutine);
        _slideCoroutine = StartCoroutine(Slide(dir));
    }

    private void UpdateCurrentGridItemPosition()
    {
        if (_currentGridItem == null) return;
        if (_currentGridItem.transform.localScale != Vector3.one) _currentGridItem.transform.localScale = Vector3.one;

        Vector3 pos = Cursor.Instance.cursor.position + new Vector3(_currentGridItem.Width / 2f * GridManager.Instance.GlobalCellSize, _currentGridItem.Height / 2f * GridManager.Instance.GlobalCellSize, -(_currentGridItem.Depth / 2f * GridManager.Instance.GlobalCellSize));
        _currentGridItem.transform.position = Vector3.Lerp(_currentGridItem.transform.position, pos, Time.deltaTime * 7);
        _currentGridItem.UpdateColor(CanPlace);
    }

    private void AddGridItemToInventory(GridItem GridItem)
    {
        if (!_inventoryItems.Contains(GridItem))
        {
            _inventoryItems.Add(GridItem);
            GridItem.transform.parent = _itemParent;
            GridItem.transform.localScale = Vector3.one * _itemScale;
            UpdateGridItemsPositionsInInventory();
        }
    }

    private void RemoveGridItemFromInventory(GridItem GridItem)
    {
        if (_inventoryItems.Contains(GridItem))
        {
            if (GridItem = _currentGridItem) _currentGridItem = null;
            _inventoryItems.Remove(GridItem);
            UpdateGridItemsPositionsInInventory();

            if (_inventoryItems.Count == 0)
            {
                //WIN
                if (WinEvent != null)
                    WinEvent.Invoke();
                //WIN
            }
        }
    }

    public void UpdateGridItemsPositionsInInventory()
    {
        float tempPosX = 0;
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            GridItem c = _inventoryItems[i];
            c.UpdateColor(true);

            float tempPos = .5f * ((c.Width * GridManager.Instance.GlobalCellSize) + _cellSpacing) * _itemScale;

            tempPosX += tempPos;

            Vector3 pos = c.transform.localPosition;
            pos.x = tempPosX;
            pos.y = 1;
            pos.z = 0;
            Vector3 scale = Vector3.one * _itemScale;

            c.SetStartTransform(pos, scale);
            tempPosX += tempPos;


            if (i != _inventoryItems.Count - 1) _maxXPos = tempPosX;
        }

    }
    private IEnumerator Slide(float direction)
    {
        Vector3 newPos = _itemParent.localPosition;
        newPos.x += (direction * 1);
        newPos.x = Mathf.Clamp(newPos.x, -_maxXPos, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 5;
            _itemParent.localPosition = Vector3.Lerp(_itemParent.localPosition, newPos, t);
            yield return null;
        }

    }

}

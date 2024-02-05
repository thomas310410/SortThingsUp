using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridBuilder : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    public Grid Grid;

    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        float cellSize = GridManager.Instance.GlobalCellSize;

        Grid = new Grid(width, height, transform.position);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = Grid.GetWorldPosition(x, y) + new Vector3(cellSize * .5f, cellSize * .5f);

                GridManager.Instance.GetCellObject(position, cellSize, this.transform.GetChild(0));
            }
        }

        BoxCollider bc = transform.gameObject.AddComponent<BoxCollider>();
        float sizeX = cellSize * width;
        float sizeY = cellSize * height;
        bc.size = new Vector3(sizeX, sizeY, .1f);
        bc.center = new Vector3(sizeX / 2, sizeY / 2);
    }

    public Vector3 GetCellPosition(Vector3 worldPosition)
    {
        return Grid.GetXY(worldPosition) * GridManager.Instance.GlobalCellSize + transform.position;
    }

    public bool CanPlace(Vector3 worldPos, GridItem item)
    {
        bool canPlace = false;
        Vector3 cellXY = Grid.GetXY(worldPos);
        if (cellXY.x + item.Width <= this.width && cellXY.y + item.Height <= this.height)
        {
            canPlace = true;
        }

        switch (item.CanPlacement)
        {
            case ItemAlign.OnlyBottom:
                if (cellXY.y != 0 && !item.BottomIsAvailable()) canPlace = false;
                break;
            case ItemAlign.OnlyTop:
                if (this.height - item.Height != cellXY.y) canPlace = false;
                break;
            case ItemAlign.BottomAndTop:
                if (cellXY.y != 0 && this.height - item.Height != cellXY.y) canPlace = false;
                break;
        }
        return canPlace;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        GridManager gm = FindObjectOfType<GridManager>();
        float cellSize = gm != null ? gm.GlobalCellSize : 0.25f; // GridManager -> Global cell size

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = (transform.position + new Vector3(cellSize / 2, cellSize / 2)) + new Vector3(x, y, 0) * cellSize;
                Gizmos.DrawWireCube(position, new Vector3(cellSize, cellSize, .01f));
            }
        }

    }
}

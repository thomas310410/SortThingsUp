using UnityEngine;

public class Grid
{
    private int width;
    private int height;
   
    private Vector3 origin;
    private int[,] gridArray;
    private GameObject[,] cellArray;

    public Grid(int width, int height, Vector3? origin = null)
    {
        this.width = width;
        this.height = height;        
        this.origin = origin != null ? origin.Value : Vector3.zero;
        gridArray = new int[width, height];
        cellArray = new GameObject[width, height];
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * GridManager.Instance.GlobalCellSize + origin;
    }

    public Vector3 GetXY(Vector3 worldPosition)
    {
        Vector3 xy= Vector3.zero;
        xy.x = Mathf.FloorToInt((worldPosition - origin).x / GridManager.Instance.GlobalCellSize);
        xy.y = Mathf.FloorToInt((worldPosition - origin).y / GridManager.Instance.GlobalCellSize);
        return xy;
    }

}

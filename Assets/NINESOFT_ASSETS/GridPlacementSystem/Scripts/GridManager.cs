
using UnityEngine;

public class GridManager : GridPlacementSystemManager<GridManager>
{
    [Header("Variables")]
    public float GlobalCellSize = .5f;
    public Material CanNotPlaceMaterial;

    [Header("Objects")]
    [SerializeField] private GameObject cellPrefab;

    public GameObject GetCellObject(Vector3 pos, float scale = 1, Transform parent = null)
    {
        GameObject g = GameObject.Instantiate(cellPrefab, pos, Quaternion.identity);
        g.transform.parent = parent;
        g.transform.localScale = Vector3.one * scale;
        return g;
    }

}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class GridItem : MonoBehaviour
{
    [Header("Variables")]
    public int Width;
    public int Height;
    public int Depth;
    public ItemAlign CanPlacement;
    [HideInInspector] public bool CanPlace;

    [Header("Private Variables")]
    private Coroutine _resetPosCoroutine;
    private Vector3 _startPos;
    private Vector3 _startScale;
    private bool _rotated;
    private bool _isHolding;
    private float _holdTime;
    private bool _placed { get { return transform.parent == null; } }

    [Header("Components")]
    private MeshRenderer[] _renderers;
    private Material[] _tempMaterials;
    private BoxCollider _col;

    [Header("Objects")]
    [SerializeField] private GameObject NormalObject;


    private void Start()
    {
        _renderers = NormalObject.GetComponentsInChildren<MeshRenderer>();

        _tempMaterials = new Material[_renderers.Length];
        for (int i = 0; i < _tempMaterials.Length; i++)
            _tempMaterials[i] = _renderers[i].material;

        _col = gameObject.GetComponent<BoxCollider>();
        Rigidbody rgd = GetComponent<Rigidbody>();
        rgd.constraints = RigidbodyConstraints.FreezeAll;
        rgd.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        CalcCollider();

        CanPlace = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("GridItem")) CanPlace = false;
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("GridItem")) CanPlace = true;
    }

    private void OnMouseDown()
    {
        _isHolding = true;
        if (_placed)
        {
            SelectMe();
        }
    }
    private void OnMouseDrag()
    {
        if (!_placed)
            if (_isHolding)
            {
                _holdTime += Time.deltaTime;
                if (_holdTime >= .15f)
                {
                    SelectMe();
                }
            }
    }


    private void OnMouseUp()
    {
        UnSelectMe();
    }

    private void SelectMe()
    {
        SlidingArea.Instance.SetCurrentGridItem(this);
    }
    private void UnSelectMe()
    {
        if (_holdTime < .15f)
        {
            if (!_placed)
                Rotate();
        }

        SlidingArea.Instance.SetCurrentGridItem(null);
        _holdTime = 0;
        _isHolding = false;
    }

    private void CalcCollider()
    {
        float cellSize = GridManager.Instance.GlobalCellSize;
        Vector3 scale = new Vector3(Width * cellSize, Height * cellSize, Depth * cellSize);

        if (_col == null) _col = gameObject.GetComponent<BoxCollider>();
        _col.size = scale * .9f;
        _col.center = Vector3.zero;
    }

    public void UpdateColor(bool canPlace)
    {
        if (NormalObject.activeSelf)
            if (_renderers != null)
                for (int i = 0; i < _renderers.Length; i++)
                    _renderers[i].material = canPlace ? _tempMaterials[i] : GridManager.Instance.CanNotPlaceMaterial;
    }
    public void SetStartTransform(Vector3 position, Vector3 scale)
    {
        _startPos = position;
        _startScale = scale;
        ResetPosition();
    }
    public void ResetPosition()
    {
        if (_resetPosCoroutine != null || Vector3.Distance(transform.localPosition, _startPos) < .1f) return;
        _resetPosCoroutine = StartCoroutine(ResetPositionEnum());
        transform.rotation = Quaternion.identity;
        UpdateColor(true);
    }

    public void Rotate()
    {
        _rotated = !_rotated;
        if (_rotated)
        {
            int temp = Width;
            Width = Depth;
            Depth = temp;
        }
        else
        {
            int temp = Depth;
            Depth = Width;
            Width = temp;
        }
        CalcCollider();
        NormalObject.transform.rotation = Quaternion.Euler(0, _rotated ? 90 : 0, 0);
        SlidingArea.Instance.UpdateGridItemsPositionsInInventory();
    }

    private IEnumerator ResetPositionEnum()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, t);
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale, t);
            yield return null;
        }
        _resetPosCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        GridManager gm = FindObjectOfType<GridManager>();
        float cellSize = gm != null ? gm.GlobalCellSize : 0.25f; // GridManager -> Global cell size

        Vector3 scale = new Vector3(Width * cellSize, Height * cellSize, Depth * cellSize);

        Gizmos.DrawWireCube(transform.position, scale * transform.localScale.x);
    }


    public bool BottomIsAvailable()
    {
        Vector3 offset = new Vector3(.2f, -.01f, -.2f);
        Debug.DrawRay(transform.position + offset, -transform.up * .25f, Color.red);
        bool b = Physics.Raycast(transform.position + offset, -transform.up, .25f, Cursor.Instance.GridItemLayer);
        return b;
    }

}

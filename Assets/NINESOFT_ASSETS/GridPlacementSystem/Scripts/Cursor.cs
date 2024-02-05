using UnityEngine;

public class Cursor : GridPlacementSystemManager<Cursor>
{
    private bool _isHolding;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _bottomLimit;
    [SerializeField] private LayerMask _defaultLayer;
    [SerializeField] private LayerMask _gridLayer;
    public LayerMask GridItemLayer;
    [HideInInspector] public Transform cursor;

    private Vector3 _worldPos;
    private RaycastHit _worldHit;
    private RaycastHit _cursorHit;
   [HideInInspector] public bool IsBelow;
    [HideInInspector] public GridBuilder CurrentGrid;

    void Start()
    {
        cursor = transform.GetChild(0);
    }

    void Update()
    {      
        _isHolding = Input.GetMouseButton(0);
        UpdateCursorPosition();
    }

    private void FixedUpdate()
    {        
        CheckWorldPosition();
    }

    private void CheckWorldPosition()
    {
        if (!_isHolding) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if (Physics.Raycast(ray, out _worldHit, 20, _defaultLayer))
        {
            bool b = (_worldHit.point.z < _bottomLimit.z) && _worldHit.collider != null;
            IsBelow = b;

            if (!b)
            {
                Ray ray1 = new Ray(cursor.position + (cursor.forward * -1f) + new Vector3(.01f, .01f), cursor.forward);

                if (Physics.Raycast(ray1, out _cursorHit, 20, _gridLayer))
                {
                    if (_cursorHit.collider != null)
                    {
                        CurrentGrid = _cursorHit.collider.GetComponent<GridBuilder>();
                    }

                }
                else CurrentGrid = null;
            }
            else CurrentGrid = null;
        }
    }

    private void UpdateCursorPosition()
    {
        if (!_isHolding) return;

        Vector3 pos = _worldHit.point + _offset;

        if (IsBelow)
        {
            _worldPos = pos;
        }
        else
        {
            _worldPos = cursor.position;
            _worldPos.x = pos.x;
            _worldPos.y = pos.y;

            if (CurrentGrid != null)
            {
                Vector3 cellPos = CurrentGrid.GetCellPosition(_worldPos);
                _worldPos = cellPos;
            } 
        }

        cursor.position = _worldPos;
    }

    public bool CanPlace(GridItem item)
    {
        return (CurrentGrid == null) ? false : CurrentGrid.CanPlace(_cursorHit.point,item);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_bottomLimit + new Vector3(-5,.1f,0),_bottomLimit+new Vector3(5,.1f,0));
    }

}

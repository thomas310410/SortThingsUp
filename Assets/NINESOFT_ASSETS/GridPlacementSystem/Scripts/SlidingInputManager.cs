using UnityEngine;

public class SlidingInputManager : GridPlacementSystemManager<SlidingInputManager>
{
    private Vector3 _startPos;
    public Vector3 moveFactor;
    private const float minX = 70;
  
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs((Input.mousePosition - _startPos).x) > minX)
            {
                moveFactor = (Input.mousePosition - _startPos).normalized;
                _startPos = Input.mousePosition;
                SlideEvent();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            moveFactor = Vector2.zero;
        }
    }

    private void SlideEvent()
    {
        SlidingArea.Instance.UpdateSlidingAreaPosition(moveFactor.x);
    }
}
